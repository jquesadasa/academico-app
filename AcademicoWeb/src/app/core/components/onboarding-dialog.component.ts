import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-onboarding-dialog',
  imports: [MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatButtonModule, MatIconModule],
  templateUrl: './onboarding-dialog.component.html',
  styleUrl: './onboarding-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OnboardingDialogComponent {}
