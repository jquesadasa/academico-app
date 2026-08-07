import { ChangeDetectionStrategy, Component, computed, inject, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { OpenApiDiscoveryService } from '../../core/services/openapi-discovery.service';
import { EntityApiService } from '../../core/services/entity-api.service';
import { ReporteAcademicoService } from '../../core/services/reporte-academico.service';
import { NotificationService } from '../../core/services/notification.service';
import { ReporteConsolidado, ReporteConsolidadoEstudiante } from '../../core/models/reporte-academico.models';

@Component({
  selector: 'app-bandas-report',
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatSelectModule
  ],
  templateUrl: './bandas-report.component.html',
  styleUrl: './bandas-report.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BandasReportComponent {
  private readonly discovery = inject(OpenApiDiscoveryService);
  private readonly entityApi = inject(EntityApiService);
  private readonly reporteApi = inject(ReporteAcademicoService);
  private readonly notification = inject(NotificationService);

  protected readonly seccionId = new FormControl<number | null>(null, [Validators.required]);
  protected readonly periodoId = new FormControl<number | null>(null, [Validators.required]);

  protected readonly secciones = signal<readonly Record<string, unknown>[]>([]);
  protected readonly periodos = signal<readonly Record<string, unknown>[]>([]);
  protected readonly cargando = signal(false);
  protected readonly reporte = signal<ReporteConsolidado | null>(null);
  protected readonly draggingFile = signal(false);
  protected readonly previewFileName = signal<string | null>(null);
  protected readonly previewFileSize = signal<string | null>(null);

  protected readonly totalEstudiantes = computed(() => this.reporte()?.estudiantes.length ?? 0);
  protected readonly estudiantes = computed<readonly ReporteConsolidadoEstudiante[]>(() => this.reporte()?.estudiantes ?? []);

  constructor() {
    void this.cargarCatalogos();
  }

  protected async generar(): Promise<void> {
    if (this.seccionId.invalid || this.periodoId.invalid) {
      this.seccionId.markAsTouched();
      this.periodoId.markAsTouched();
      return;
    }

    this.cargando.set(true);

    try {
      const data = await this.reporteApi.getConsolidado(this.seccionId.value ?? 0, this.periodoId.value ?? 0);
      this.reporte.set(data);
      this.notification.success('Reporte consolidado generado.');
    } finally {
      this.cargando.set(false);
    }
  }

  protected async exportarCsv(): Promise<void> {
    if (this.seccionId.invalid || this.periodoId.invalid) {
      this.seccionId.markAsTouched();
      this.periodoId.markAsTouched();
      return;
    }

    const blob = await this.reporteApi.exportarConsolidadoCsv(this.seccionId.value ?? 0, this.periodoId.value ?? 0);
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = `reporte-bandas-seccion-${this.seccionId.value}-periodo-${this.periodoId.value}.csv`;
    anchor.click();
    URL.revokeObjectURL(url);
    this.notification.success('Archivo CSV generado.');
  }

  protected onDragOver(event: DragEvent): void {
    event.preventDefault();
    this.draggingFile.set(true);
  }

  protected onDragLeave(event: DragEvent): void {
    event.preventDefault();
    this.draggingFile.set(false);
  }

  protected onDrop(event: DragEvent): void {
    event.preventDefault();
    this.draggingFile.set(false);

    const file = event.dataTransfer?.files?.item(0);
    if (!file) {
      return;
    }

    this.setFilePreview(file);
  }

  protected onFileInputChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.item(0);

    if (!file) {
      return;
    }

    this.setFilePreview(file);
  }

  protected numberValue(value: unknown): number {
    return Number(value ?? 0);
  }

  protected text(value: unknown): string {
    if (value === null || value === undefined || value === '') {
      return '-';
    }

    return String(value);
  }

  private async cargarCatalogos(): Promise<void> {
    const seccionesEntity = this.discovery.getEntity('secciones');
    const periodosEntity = this.discovery.getEntity('periodos');

    if (!seccionesEntity || !periodosEntity) {
      this.notification.error('No se pudieron cargar secciones y periodos desde la API.');
      return;
    }

    const [secciones, periodos] = await Promise.all([
      this.entityApi.list(seccionesEntity),
      this.entityApi.list(periodosEntity)
    ]);

    this.secciones.set(secciones);
    this.periodos.set(periodos);
  }

  private setFilePreview(file: File): void {
    this.previewFileName.set(file.name);
    this.previewFileSize.set(`${(file.size / 1024).toFixed(1)} KB`);
    this.notification.success(`Archivo detectado: ${file.name}`);
  }
}
