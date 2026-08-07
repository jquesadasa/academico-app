export interface OpenApiDocument {
  readonly paths: Record<string, Record<string, OpenApiOperation>>;
  readonly components?: {
    readonly schemas?: Record<string, OpenApiSchema>;
  };
}

export interface OpenApiOperation {
  readonly tags?: readonly string[];
  readonly parameters?: readonly OpenApiParameter[];
  readonly requestBody?: {
    readonly content?: Record<string, OpenApiMediaType>;
  };
  readonly responses?: Record<string, OpenApiResponse>;
}

export interface OpenApiResponse {
  readonly content?: Record<string, OpenApiMediaType>;
}

export interface OpenApiMediaType {
  readonly schema?: OpenApiSchema;
}

export interface OpenApiParameter {
  readonly name: string;
  readonly in: string;
  readonly required?: boolean;
  readonly schema?: OpenApiSchema;
}

export interface OpenApiSchema {
  readonly $ref?: string;
  readonly type?: string;
  readonly format?: string;
  readonly properties?: Record<string, OpenApiSchema>;
  readonly items?: OpenApiSchema;
}

export interface FieldDefinition {
  readonly name: string;
  readonly type: 'text' | 'number' | 'date' | 'boolean';
  readonly required: boolean;
  readonly readOnly: boolean;
}

export interface EntityDefinition {
  readonly key: string;
  readonly label: string;
  readonly basePath: string;
  readonly schemaName: string;
  readonly fields: readonly FieldDefinition[];
  readonly supportsCreate: boolean;
  readonly supportsUpdate: boolean;
  readonly supportsDelete: boolean;
}
