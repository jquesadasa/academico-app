import { ChangeDetectionStrategy, Component, DestroyRef, computed, inject, signal } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterOutlet } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { filter } from 'rxjs/operators';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { OpenApiDiscoveryService } from '../core/services/openapi-discovery.service';
import { ThemeService } from '../core/services/theme.service';
import { LoadingService } from '../core/services/loading.service';
import { KeyboardShortcutsService } from '../core/services/keyboard-shortcuts.service';

@Component({
  selector: 'app-shell',
  imports: [
    RouterOutlet,
    RouterLink,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatProgressBarModule,
    MatTooltipModule
  ],
  templateUrl: './shell.component.html',
  styleUrl: './shell.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ShellComponent {
  private readonly router = inject(Router);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly discoveryService = inject(OpenApiDiscoveryService);
  protected readonly themeService = inject(ThemeService);
  protected readonly loadingService = inject(LoadingService);
  protected readonly keyboardShortcutsService = inject(KeyboardShortcutsService);

  protected readonly collapsed = signal(false);
  private readonly currentUrl = signal(this.router.url);

  protected readonly sideNavTooltip = computed(() =>
    this.collapsed() ? 'Mostrar menu lateral' : 'Ocultar menu lateral'
  );

  protected readonly themeTooltip = computed(() =>
    this.themeService.darkMode() ? 'Cambiar a modo claro' : 'Cambiar a modo oscuro'
  );

  protected readonly breadcrumbs = computed(() => {
    return this.currentUrl()
      .split('?')[0]
      .split('/')
      .filter(Boolean)
      .map((segment, index, all) => {
        const url = `/${all.slice(0, index + 1).join('/')}`;
        return {
          label: segment.replace(/-/g, ' '),
          url
        };
      });
  });

  constructor() {
    this.router.events
      .pipe(
        filter((event): event is NavigationEnd => event instanceof NavigationEnd),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe((event) => this.currentUrl.set(event.urlAfterRedirects));
  }

  protected toggleSideNav(): void {
    this.collapsed.update((value) => !value);
  }

  protected openCommandPalette(): void {
    this.keyboardShortcutsService.openCommandPalette();
  }
}
