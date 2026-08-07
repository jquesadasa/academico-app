import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../../environments/environment';
import { EntityDefinition } from '../models/openapi.models';

@Injectable({ providedIn: 'root' })
export class EntityApiService {
  private readonly http = inject(HttpClient);

  async list(entity: EntityDefinition): Promise<readonly Record<string, unknown>[]> {
    const url = this.toUrl(entity.basePath);
    return firstValueFrom(this.http.get<readonly Record<string, unknown>[]>(url));
  }

  async getById(entity: EntityDefinition, id: string | number): Promise<Record<string, unknown>> {
    const url = this.toUrl(`${entity.basePath}/${encodeURIComponent(String(id))}`);
    return firstValueFrom(this.http.get<Record<string, unknown>>(url));
  }

  async create(entity: EntityDefinition, payload: Record<string, unknown>): Promise<Record<string, unknown>> {
    const url = this.toUrl(entity.basePath);
    return firstValueFrom(this.http.post<Record<string, unknown>>(url, payload));
  }

  async update(entity: EntityDefinition, id: string | number, payload: Record<string, unknown>): Promise<void> {
    const url = this.toUrl(`${entity.basePath}/${encodeURIComponent(String(id))}`);
    await firstValueFrom(this.http.put<void>(url, payload));
  }

  async delete(entity: EntityDefinition, id: string | number): Promise<void> {
    const url = this.toUrl(`${entity.basePath}/${encodeURIComponent(String(id))}`);
    await firstValueFrom(this.http.delete<void>(url));
  }

  private toUrl(path: string): string {
    return `${environment.apiBaseUrl}${path}`;
  }
}
