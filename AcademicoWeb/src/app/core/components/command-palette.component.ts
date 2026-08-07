import { ChangeDetectionStrategy, Component, DestroyRef, computed, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { OpenApiDiscoveryService } from '../services/openapi-discovery.service';

interface CommandItem {
  readonly id: string;
  readonly label: string;
  readonly description: string;
  readonly route: string;
  readonly icon: string;
}

@Component({
  selector: 'app-command-palette',
  imports: [
    ReactiveFormsModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatListModule,
    MatIconModule
  ],
  templateUrl: './command-palette.component.html',
  styleUrl: './command-palette.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CommandPaletteComponent {
  private readonly router = inject(Router);
  private readonly dialogRef = inject(MatDialogRef<CommandPaletteComponent>);
  private readonly discovery = inject(OpenApiDiscoveryService);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly searchControl = new FormControl('', { nonNullable: true });
  private readonly searchValue = signal('');

  protected readonly commands = computed(() => {
    const staticCommands: readonly CommandItem[] = [
      {
        id: 'dashboard',
        label: 'Ir al dashboard',
        description: 'Vista general de entidades disponibles',
        route: '/dashboard',
        icon: 'dashboard'
      },
      {
        id: 'reportes-bandas',
        label: 'Abrir reporte de bandas',
        description: 'Consolidado academico por seccion y periodo',
        route: '/reportes/bandas',
        icon: 'assessment'
      }
    ];

    const entityCommands = this.discovery.entities().map((entity) => ({
      id: entity.key,
      label: `Mantenimiento: ${entity.label}`,
      description: `CRUD de ${entity.label}`,
      route: `/mantenimientos/${entity.key}`,
      icon: 'dataset'
    }));

    const all = [...staticCommands, ...entityCommands];
    const query = this.searchValue().trim().toLowerCase();

    if (!query) {
      return all;
    }

    return all.filter((item) => `${item.label} ${item.description}`.toLowerCase().includes(query));
  });

  constructor() {
    this.searchControl.valueChanges
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((value) => this.searchValue.set(value));
  }

  protected async open(item: CommandItem): Promise<void> {
    await this.router.navigateByUrl(item.route);
    this.dialogRef.close();
  }
}
