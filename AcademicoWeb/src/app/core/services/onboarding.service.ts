import { Injectable, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { take } from 'rxjs';
import { OnboardingDialogComponent } from '../components/onboarding-dialog.component';

const ONBOARDING_KEY = 'academico-onboarding-v1';

@Injectable({ providedIn: 'root' })
export class OnboardingService {
  private readonly dialog = inject(MatDialog);
  private readonly storage = this.resolveStorage();

  showOnFirstVisit(): void {
    if (!this.storage) {
      return;
    }

    if (this.storage.getItem(ONBOARDING_KEY) === 'done') {
      return;
    }

    this.dialog
      .open(OnboardingDialogComponent, {
        width: '720px',
        maxWidth: '95vw',
        panelClass: 'onboarding-dialog'
      })
      .afterClosed()
      .pipe(take(1))
      .subscribe(() => this.storage?.setItem(ONBOARDING_KEY, 'done'));
  }

  private resolveStorage(): Storage | null {
    const candidate = (globalThis as { localStorage?: Partial<Storage> }).localStorage;

    if (!candidate) {
      return null;
    }

    if (typeof candidate.getItem !== 'function' || typeof candidate.setItem !== 'function') {
      return null;
    }

    return candidate as Storage;
  }
}
