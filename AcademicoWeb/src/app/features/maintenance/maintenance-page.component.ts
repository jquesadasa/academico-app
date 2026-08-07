import { AfterViewInit, ChangeDetectionStrategy, Component, computed, inject, signal, viewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { OpenApiDiscoveryService } from '../../core/services/openapi-discovery.service';
import { EntityApiService } from '../../core/services/entity-api.service';
import { NotificationService } from '../../core/services/notification.service';
import { ConfirmDialogComponent } from './components/confirm-dialog.component';
import { EntityFormDialogComponent } from './components/entity-form-dialog.component';
import { EntityDetailDialogComponent } from './components/entity-detail-dialog.component';

@Component({
  selector: 'app-maintenance-page',
  imports: [
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule
  ],
  templateUrl: './maintenance-page.component.html',
  styleUrl: './maintenance-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MaintenancePageComponent implements AfterViewInit {
  private readonly route = inject(ActivatedRoute);
  private readonly discoveryService = inject(OpenApiDiscoveryService);
  private readonly api = inject(EntityApiService);
  private readonly dialog = inject(MatDialog);
  private readonly notificationService = inject(NotificationService);

  protected readonly dataSource = new MatTableDataSource<Record<string, unknown>>([]);
  protected readonly filterValue = signal('');
  protected readonly isLoaded = signal(false);

  protected readonly paginator = viewChild(MatPaginator);
  protected readonly sort = viewChild(MatSort);

  protected readonly entityKey = computed(() => this.route.snapshot.paramMap.get('entity') ?? '');
  protected readonly entity = computed(() => this.discoveryService.getEntity(this.entityKey()));

  protected readonly displayedColumns = computed(() => {
    const entity = this.entity();
    if (!entity) {
      return ['actions'];
    }

    const visibleFields = entity.fields
      .filter((field) => field.name !== 'id')
      .slice(0, 6)
      .map((field) => field.name);

    return ['id', ...visibleFields, 'actions'];
  });

  constructor() {
    this.refresh();
    this.dataSource.filterPredicate = (row, filter) => JSON.stringify(row).toLowerCase().includes(filter);
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator() ?? null;
    this.dataSource.sort = this.sort() ?? null;
  }

  protected applyFilter(value: string): void {
    this.filterValue.set(value);
    this.dataSource.filter = value.trim().toLowerCase();
  }

  protected async create(): Promise<void> {
    const entity = this.entity();
    if (!entity) {
      return;
    }

    const payload = await this.dialog
      .open(EntityFormDialogComponent, {
        data: {
          title: `Nuevo ${entity.label}`,
          fields: entity.fields
        }
      })
      .afterClosed()
      .toPromise();

    if (!payload) {
      return;
    }

    await this.api.create(entity, payload as Record<string, unknown>);
    this.notificationService.success('Registro creado exitosamente.');
    await this.refresh();
  }

  protected async edit(item: Record<string, unknown>): Promise<void> {
    const entity = this.entity();
    if (!entity) {
      return;
    }

    const id = item['id'];
    if (id === null || id === undefined) {
      this.notificationService.error('No se pudo identificar el registro a editar.');
      return;
    }

    let fullItem = item;
    try {
      const detail = await this.api.getById(entity, String(id));
      fullItem = { ...item, ...detail };
    } catch {
      // If detail endpoint is not available, keep grid data as fallback.
      fullItem = item;
    }

    const payload = await this.dialog
      .open(EntityFormDialogComponent, {
        data: {
          title: `Editar ${entity.label}`,
          fields: entity.fields,
          item: fullItem
        }
      })
      .afterClosed()
      .toPromise();

    if (!payload) {
      return;
    }

    await this.api.update(entity, String(id), {
      ...fullItem,
      ...(payload as Record<string, unknown>),
      id
    });
    this.notificationService.success('Registro actualizado.');
    await this.refresh();
  }

  protected view(item: Record<string, unknown>): void {
    const entity = this.entity();
    if (!entity) {
      return;
    }

    this.dialog.open(EntityDetailDialogComponent, {
      data: {
        title: `Detalle ${entity.label}`,
        item
      }
    });
  }

  protected async remove(item: Record<string, unknown>): Promise<void> {
    const entity = this.entity();
    if (!entity) {
      return;
    }

    const id = item['id'];
    if (id === null || id === undefined) {
      this.notificationService.error('No se pudo identificar el registro a eliminar.');
      return;
    }

    const confirmed = await this.dialog
      .open(ConfirmDialogComponent, {
        data: {
          title: 'Confirmar eliminacion',
          message: 'Esta accion no se puede deshacer. Deseas continuar?'
        }
      })
      .afterClosed()
      .toPromise();

    if (!confirmed) {
      return;
    }

    await this.api.delete(entity, String(id));
    this.notificationService.success('Registro eliminado.');
    await this.refresh();
  }

  protected cell(item: Record<string, unknown>, key: string): string {
    const value = item[key];

    if (value === null || value === undefined) {
      return '-';
    }

    return String(value);
  }

  protected async refresh(): Promise<void> {
    this.isLoaded.set(false);

    const entity = this.entity();
    if (!entity) {
      this.dataSource.data = [];
      this.isLoaded.set(true);
      return;
    }

    const data = await this.api.list(entity);
    this.dataSource.data = [...data];
    this.isLoaded.set(true);
  }
}
