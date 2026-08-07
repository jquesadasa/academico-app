import { Injectable, effect, signal } from '@angular/core';

const THEME_KEY = 'academico-theme';

@Injectable({ providedIn: 'root' })
export class ThemeService {
  readonly darkMode = signal(localStorage.getItem(THEME_KEY) === 'dark');

  constructor() {
    effect(() => {
      const isDark = this.darkMode();
      document.documentElement.classList.toggle('dark-mode', isDark);
      localStorage.setItem(THEME_KEY, isDark ? 'dark' : 'light');
    });
  }

  toggle(): void {
    this.darkMode.update((value) => !value);
  }
}
