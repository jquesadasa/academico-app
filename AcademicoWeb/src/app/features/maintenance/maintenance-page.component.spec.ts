import { of } from 'rxjs';
import { convertToParamMap, ActivatedRoute } from '@angular/router';
import { TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialog } from '@angular/material/dialog';
import { MaintenancePageComponent } from './maintenance-page.component';
import { OpenApiDiscoveryService } from '../../core/services/openapi-discovery.service';
import { EntityApiService } from '../../core/services/entity-api.service';
import { NotificationService } from '../../core/services/notification.service';

const entityDefinition = {
  key: 'estudiantes',
  label: 'Estudiantes',
  basePath: '/api/v1/Estudiantes',
  schemaName: 'EstudianteDto',
  fields: [
    { name: 'id', type: 'number', required: false, readOnly: true },
    { name: 'cedula', type: 'text', required: true, readOnly: false },
    { name: 'nombre', type: 'text', required: true, readOnly: false }
  ],
  supportsCreate: true,
  supportsUpdate: true,
  supportsDelete: true
};

describe('MaintenancePageComponent', () => {
  const apiMock = {
    list: vi.fn().mockResolvedValue([{ id: 1, cedula: '1-111', nombre: 'Ana' }]),
    getById: vi.fn().mockResolvedValue({ id: 1, cedula: '1-111', nombre: 'Ana', primerApellido: 'Lopez' }),
    create: vi.fn().mockResolvedValue({ id: 2 }),
    update: vi.fn().mockResolvedValue(undefined),
    delete: vi.fn().mockResolvedValue(undefined)
  };

  const dialogMock = {
    open: vi.fn()
  };

  const notificationMock = {
    success: vi.fn(),
    error: vi.fn()
  };

  beforeEach(async () => {
    apiMock.list.mockClear();
    apiMock.getById.mockClear();
    apiMock.create.mockClear();
    apiMock.update.mockClear();
    apiMock.delete.mockClear();
    dialogMock.open.mockClear();
    notificationMock.success.mockClear();

    await TestBed.configureTestingModule({
      imports: [MaintenancePageComponent, NoopAnimationsModule],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: convertToParamMap({ entity: 'estudiantes' })
            }
          }
        },
        {
          provide: OpenApiDiscoveryService,
          useValue: {
            getEntity: vi.fn().mockReturnValue(entityDefinition)
          }
        },
        { provide: EntityApiService, useValue: apiMock },
        { provide: MatDialog, useValue: dialogMock },
        { provide: NotificationService, useValue: notificationMock }
      ]
    }).compileComponents();
  });

  it('loads data on init', async () => {
    const fixture = TestBed.createComponent(MaintenancePageComponent);
    await fixture.whenStable();

    const component = fixture.componentInstance as any;
    expect(component.dataSource.data.length).toBe(1);
    expect(apiMock.list).toHaveBeenCalledTimes(1);
  });

  it('applies filter to datasource', async () => {
    const fixture = TestBed.createComponent(MaintenancePageComponent);
    await fixture.whenStable();

    const component = fixture.componentInstance as any;
    component.applyFilter('ana');

    expect(component.filterValue()).toBe('ana');
    expect(component.dataSource.filter).toBe('ana');
  });

  it('creates a record when dialog returns payload', async () => {
    dialogMock.open.mockReturnValue({
      afterClosed: () => of({ cedula: '2-222', nombre: 'Luis' })
    });

    const fixture = TestBed.createComponent(MaintenancePageComponent);
    await fixture.whenStable();

    const component = fixture.componentInstance as any;
    await component.create();

    expect(apiMock.create).toHaveBeenCalledTimes(1);
    expect(notificationMock.success).toHaveBeenCalled();
  });

  it('removes a record when user confirms', async () => {
    dialogMock.open.mockReturnValue({
      afterClosed: () => of(true)
    });

    const fixture = TestBed.createComponent(MaintenancePageComponent);
    await fixture.whenStable();

    const component = fixture.componentInstance as any;
    await component.remove({ id: 1 });

    expect(apiMock.delete).toHaveBeenCalledWith(entityDefinition, '1');
    expect(notificationMock.success).toHaveBeenCalled();
  });

  it('loads full item before edit dialog and updates merged payload', async () => {
    dialogMock.open.mockReturnValue({
      afterClosed: () => of({ nombre: 'Ana Maria' })
    });

    const fixture = TestBed.createComponent(MaintenancePageComponent);
    await fixture.whenStable();

    const component = fixture.componentInstance as any;
    await component.edit({ id: 1, cedula: '1-111' });

    expect(apiMock.getById).toHaveBeenCalledWith(entityDefinition, '1');
    expect(apiMock.update).toHaveBeenCalledWith(
      entityDefinition,
      '1',
      expect.objectContaining({
        id: 1,
        cedula: '1-111',
        primerApellido: 'Lopez',
        nombre: 'Ana Maria'
      })
    );
  });
});
