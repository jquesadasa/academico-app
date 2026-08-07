import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ReporteConsolidado } from '../models/reporte-academico.models';

@Injectable({ providedIn: 'root' })
export class ReporteAcademicoService {
  private readonly http = inject(HttpClient);

  async getConsolidado(seccionId: number, periodoId: number): Promise<ReporteConsolidado> {
    const url = `${environment.apiBaseUrl}/api/v${environment.apiVersion}/reportesacademicos/consolidado/seccion/${seccionId}/periodo/${periodoId}`;
    return firstValueFrom(this.http.get<ReporteConsolidado>(url));
  }

  async exportarConsolidadoCsv(seccionId: number, periodoId: number): Promise<Blob> {
    const url = `${environment.apiBaseUrl}/api/v${environment.apiVersion}/reportesacademicos/consolidado/seccion/${seccionId}/periodo/${periodoId}/csv`;
    return firstValueFrom(this.http.get(url, { responseType: 'blob' }));
  }
}
