import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { OpenApiDiscoveryService } from './openapi-discovery.service';

describe('OpenApiDiscoveryService', () => {
  let service: OpenApiDiscoveryService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()]
    });

    service = TestBed.inject(OpenApiDiscoveryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('loads swagger and maps entities', async () => {
    const loadPromise = service.load();

    const req = httpMock.expectOne('http://localhost:5219/swagger/v1/swagger.json');
    expect(req.request.method).toBe('GET');

    req.flush({
      paths: {
        '/api/v{version}/Estudiantes': {
          get: {
            responses: {
              '200': {
                content: {
                  'application/json': {
                    schema: {
                      type: 'array',
                      items: { $ref: '#/components/schemas/EstudianteDto' }
                    }
                  }
                }
              }
            }
          },
          post: {
            requestBody: {
              content: {
                'application/json': {
                  schema: { $ref: '#/components/schemas/EstudianteDto' }
                }
              }
            }
          }
        },
        '/api/v{version}/Estudiantes/{id}': {
          put: {
            requestBody: {
              content: {
                'application/json': {
                  schema: { $ref: '#/components/schemas/EstudianteDto' }
                }
              }
            }
          },
          delete: {
            responses: { '200': { description: 'OK' } }
          }
        }
      },
      components: {
        schemas: {
          EstudianteDto: {
            type: 'object',
            properties: {
              id: { type: 'integer' },
              cedula: { type: 'string' },
              activo: { type: 'boolean' },
              createdAt: { type: 'string', format: 'date-time' }
            }
          }
        }
      }
    });

    await loadPromise;

    expect(service.isReady()).toBe(true);

    const entity = service.getEntity('estudiantes');
    expect(entity).toBeTruthy();
    expect(entity?.supportsCreate).toBe(true);
    expect(entity?.supportsUpdate).toBe(true);
    expect(entity?.supportsDelete).toBe(true);
    expect(entity?.fields.find((field) => field.name === 'activo')?.type).toBe('boolean');
    expect(entity?.fields.find((field) => field.name === 'createdAt')?.readOnly).toBe(true);
  });

  it('does not call swagger twice when already loaded', async () => {
    const firstLoad = service.load();
    httpMock.expectOne('http://localhost:5219/swagger/v1/swagger.json').flush({ paths: {} });
    await firstLoad;

    await service.load();

    httpMock.expectNone('http://localhost:5219/swagger/v1/swagger.json');
  });
});
