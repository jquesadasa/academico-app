import { Component, signal } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { provideRouter, Router } from '@angular/router';
import { ShellComponent } from './shell.component';
import { OpenApiDiscoveryService } from '../core/services/openapi-discovery.service';
import { ThemeService } from '../core/services/theme.service';
import { LoadingService } from '../core/services/loading.service';

@Component({ template: '' })
class DummyComponent {}

class MockOpenApiDiscoveryService {
  readonly entities = signal([{ key: 'estudiantes', label: 'Estudiantes' }]);
}

class MockThemeService {
  readonly darkMode = signal(false);

  toggle(): void {
    this.darkMode.update((value) => !value);
  }
}

class MockLoadingService {
  readonly isLoading = signal(false);
}

describe('ShellComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShellComponent],
      providers: [
        provideRouter([
          { path: 'dashboard', component: DummyComponent },
          { path: 'mantenimientos/:entity', component: DummyComponent }
        ]),
        { provide: OpenApiDiscoveryService, useClass: MockOpenApiDiscoveryService },
        { provide: ThemeService, useClass: MockThemeService },
        { provide: LoadingService, useClass: MockLoadingService }
      ]
    }).compileComponents();
  });

  it('creates component', () => {
    const fixture = TestBed.createComponent(ShellComponent);
    expect(fixture.componentInstance).toBeTruthy();
  });

  it('toggles side nav state', () => {
    const fixture = TestBed.createComponent(ShellComponent);
    const component = fixture.componentInstance as any;

    expect(component.collapsed()).toBe(false);
    component.toggleSideNav();
    expect(component.collapsed()).toBe(true);
  });

  it('updates breadcrumbs on route change', async () => {
    const fixture = TestBed.createComponent(ShellComponent);
    const router = TestBed.inject(Router);

    await router.navigateByUrl('/mantenimientos/estudiantes');
    fixture.detectChanges();

    const breadcrumbs = (fixture.componentInstance as any).breadcrumbs();
    expect(breadcrumbs.length).toBe(2);
    expect(breadcrumbs[0].label).toBe('mantenimientos');
    expect(breadcrumbs[1].label).toBe('estudiantes');
  });
});
