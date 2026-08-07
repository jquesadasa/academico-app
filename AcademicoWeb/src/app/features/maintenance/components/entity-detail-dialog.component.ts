import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

interface EntityDetailData {
  readonly title: string;
  readonly item: Record<string, unknown>;
}

@Component({
  selector: 'app-entity-detail-dialog',
  imports: [MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatButtonModule],
  template: `
    <h2 mat-dialog-title>{{ data.title }}</h2>
    <mat-dialog-content>
      <table>
        @for (entry of entries(); track entry.key) {
          <tr>
            <th>{{ entry.key }}</th>
            <td>{{ entry.value }}</td>
          </tr>
        }
      </table>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-flat-button mat-dialog-close>Cerrar</button>
    </mat-dialog-actions>
  `,
  styles: [
    `
      table {
        width: 100%;
        border-collapse: collapse;
      }

      tr th,
      tr td {
        border: 1px solid #e6e6e6;
        padding: 0.5rem;
        text-align: left;
      }

      tr th {
        background-color: #f5f5f5;
      }
    `
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EntityDetailDialogComponent {
  protected readonly data = inject<EntityDetailData>(MAT_DIALOG_DATA);

  protected readonly entries = () => Object.entries(this.data.item).map(([key, value]) => ({ key, value: String(value ?? '') }));
}
