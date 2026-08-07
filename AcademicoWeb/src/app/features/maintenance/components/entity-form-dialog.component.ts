import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FieldDefinition } from '../../../core/models/openapi.models';

interface EntityFormDialogData {
  readonly title: string;
  readonly fields: readonly FieldDefinition[];
  readonly item?: Record<string, unknown>;
}

@Component({
  selector: 'app-entity-form-dialog',
  imports: [
    ReactiveFormsModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatCheckboxModule
  ],
  templateUrl: './entity-form-dialog.component.html',
  styleUrl: './entity-form-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EntityFormDialogComponent {
  protected readonly data = inject<EntityFormDialogData>(MAT_DIALOG_DATA);
  private readonly dialogRef = inject(MatDialogRef<EntityFormDialogComponent>);

  protected readonly editableFields = this.data.fields.filter((field) => !field.readOnly);
  protected readonly form = this.buildForm();

  protected submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.dialogRef.close(this.form.getRawValue());
  }

  private buildForm(): FormGroup<Record<string, FormControl<unknown>>> {
    const controls: Record<string, FormControl<unknown>> = {};

    for (const field of this.editableFields) {
      const currentValue = this.data.item?.[field.name] ?? this.defaultValue(field.type);
      controls[field.name] = new FormControl(currentValue, {
        nonNullable: false,
        validators: field.required ? [Validators.required] : []
      });
    }

    return new FormGroup(controls);
  }

  private defaultValue(type: FieldDefinition['type']): unknown {
    if (type === 'boolean') {
      return false;
    }

    if (type === 'number') {
      return null;
    }

    return '';
  }
}
