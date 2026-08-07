import { Injectable, computed, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import {
  EntityDefinition,
  FieldDefinition,
  OpenApiDocument,
  OpenApiOperation,
  OpenApiSchema
} from '../models/openapi.models';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class OpenApiDiscoveryService {
  private readonly http = inject(HttpClient);

  private readonly entitiesSignal = signal<readonly EntityDefinition[]>([]);
  readonly entities = computed(() => this.entitiesSignal());

  private readonly isReadySignal = signal(false);
  readonly isReady = computed(() => this.isReadySignal());

  async load(): Promise<void> {
    if (this.isReadySignal()) {
      return;
    }

    const document = await firstValueFrom(
      this.http.get<OpenApiDocument>(`${environment.apiBaseUrl}${environment.openApiPath}`)
    );

    this.entitiesSignal.set(this.parseEntities(document));
    this.isReadySignal.set(true);
  }

  getEntity(key: string): EntityDefinition | undefined {
    return this.entitiesSignal().find((entity) => entity.key === key);
  }

  private parseEntities(document: OpenApiDocument): readonly EntityDefinition[] {
    const entities = new Map<string, EntityDefinition>();
    const schemas = document.components?.schemas ?? {};

    for (const [path, methods] of Object.entries(document.paths)) {
      const normalizedPath = path.replace('v{version}', `v${environment.apiVersion}`);
      const segments = normalizedPath.split('/').filter(Boolean);

      if (segments.length < 3 || segments[0] !== 'api') {
        continue;
      }

      const entitySegment = segments[2];
      const key = entitySegment.toLowerCase();
      const current = entities.get(key);

      const getOp = methods['get'];
      const postOp = methods['post'];
      const putOp = methods['put'];
      const deleteOp = methods['delete'];

      const schemaName =
        this.extractSchemaName(getOp) ??
        this.extractSchemaName(postOp) ??
        this.extractSchemaName(putOp) ??
        current?.schemaName ??
        `${entitySegment.slice(0, -1)}Dto`;

      const fields = this.extractFields(schemas[schemaName]);

      if (!current) {
        entities.set(key, {
          key,
          label: entitySegment,
          basePath: `/api/v${environment.apiVersion}/${entitySegment}`,
          schemaName,
          fields,
          supportsCreate: Boolean(postOp),
          supportsUpdate: Boolean(putOp),
          supportsDelete: Boolean(deleteOp)
        });
      } else {
        entities.set(key, {
          ...current,
          supportsCreate: current.supportsCreate || Boolean(postOp),
          supportsUpdate: current.supportsUpdate || Boolean(putOp),
          supportsDelete: current.supportsDelete || Boolean(deleteOp),
          fields: current.fields.length > 0 ? current.fields : fields
        });
      }
    }

    return [...entities.values()].sort((a, b) => a.label.localeCompare(b.label));
  }

  private extractSchemaName(operation: OpenApiOperation | undefined): string | undefined {
    if (!operation?.responses) {
      return this.extractSchemaNameFromBody(operation);
    }

    for (const response of Object.values(operation.responses)) {
      const schemaName = this.extractSchemaRef(response.content);
      if (schemaName) {
        return schemaName;
      }
    }

    return this.extractSchemaNameFromBody(operation);
  }

  private extractSchemaNameFromBody(operation: OpenApiOperation | undefined): string | undefined {
    if (!operation?.requestBody?.content) {
      return undefined;
    }

    return this.extractSchemaRef(operation.requestBody.content);
  }

  private extractSchemaRef(content: Record<string, { schema?: OpenApiSchema }> | undefined): string | undefined {
    if (!content) {
      return undefined;
    }

    for (const mediaType of Object.values(content)) {
      const schemaRef = mediaType.schema?.$ref;
      if (schemaRef?.startsWith('#/components/schemas/')) {
        return schemaRef.split('/').at(-1);
      }

      const itemRef = mediaType.schema?.items?.$ref;
      if (itemRef?.startsWith('#/components/schemas/')) {
        return itemRef.split('/').at(-1);
      }
    }

    return undefined;
  }

  private extractFields(schema: OpenApiSchema | undefined): readonly FieldDefinition[] {
    if (!schema?.properties) {
      return [];
    }

    const readOnlyNames = new Set([
      'id',
      'createdAt',
      'updatedAt',
      'nombreCompleto',
      'estudianteNombreCompleto',
      'asignaturaNombre',
      'esBI',
      'esNacional',
      'aprobado',
      'totalAusentismo',
      'condicion'
    ]);

    return Object.entries(schema.properties).map(([name, property]) => {
      const type = this.mapInputType(property);

      return {
        name,
        type,
        required: name !== 'id' && !name.endsWith('Id') ? type !== 'boolean' : false,
        readOnly: readOnlyNames.has(name)
      };
    });
  }

  private mapInputType(schema: OpenApiSchema): FieldDefinition['type'] {
    if (schema.type === 'boolean') {
      return 'boolean';
    }

    if (schema.type === 'integer' || schema.type === 'number') {
      return 'number';
    }

    if (schema.format === 'date-time' || schema.format === 'date') {
      return 'date';
    }

    return 'text';
  }
}
