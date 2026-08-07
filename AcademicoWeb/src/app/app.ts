import { ChangeDetectionStrategy, Component, HostListener, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AudioService } from './core/services/audio.service';
import { KeyboardShortcutsService } from './core/services/keyboard-shortcuts.service';
import { OnboardingService } from './core/services/onboarding.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class App {
  private readonly audioService = inject(AudioService);
  private readonly keyboardShortcutsService = inject(KeyboardShortcutsService);
  private readonly onboardingService = inject(OnboardingService);

  constructor() {
    void this.keyboardShortcutsService;
    queueMicrotask(() => this.onboardingService.showOnFirstVisit());
  }

  @HostListener('document:click', ['$event'])
  protected onDocumentClick(event: MouseEvent): void {
    const target = event.target as HTMLElement | null;
    if (!target) {
      return;
    }

    const isInteractive = target.closest('button, a, mat-chip-option, [role="button"], .mat-mdc-tab-link');
    if (isInteractive) {
      this.audioService.playClick();
    }
  }
}
