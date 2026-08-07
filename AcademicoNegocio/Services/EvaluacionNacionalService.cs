using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class EvaluacionNacionalService : IEvaluacionNacionalService
{
    private readonly IEvaluacionNacionalRepository _repository;

    public EvaluacionNacionalService(IEvaluacionNacionalRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EvaluacionNacionalDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<EvaluacionNacionalDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<EvaluacionNacionalDto>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default)
        => (await _repository.GetByEstudianteAsync(estudianteId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<IReadOnlyList<EvaluacionNacionalDto>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default)
        => (await _repository.GetBySeccionAsync(seccionId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<IReadOnlyList<EvaluacionNacionalDto>> GetBySeccionYPeriodoAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default)
        => (await _repository.GetBySeccionYPeriodoAsync(seccionId, periodoId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<EvaluacionNacionalDto?> GetByEstudianteAsignaturaSeccionAsync(Guid estudianteId, Guid asignaturaId, Guid seccionId, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByEstudianteAsignaturaSeccionAsync(estudianteId, asignaturaId, seccionId, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<EvaluacionNacionalDto>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default)
        => (await _repository.GetConsolidadoGrupalAsync(seccionId, periodoId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<EvaluacionNacionalDto> CreateAsync(EvaluacionNacionalDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, EvaluacionNacionalDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.EstudianteId = dto.EstudianteId;
        existing.AsignaturaId = dto.AsignaturaId;
        existing.SeccionId = dto.SeccionId;
        existing.PeriodoId = dto.PeriodoId;
        existing.NotaMinima = dto.NotaMinima;
        existing.NotaObtenida = dto.NotaObtenida;
        existing.NotaPruebaEstandarizada = dto.NotaPruebaEstandarizada;
        existing.AusentismoTardias = dto.AusentismoTardias;
        existing.AusentismoInjustificadas = dto.AusentismoInjustificadas;
        existing.AusentismoJustificadas = dto.AusentismoJustificadas;
        existing.Observaciones = dto.Observaciones;
        existing.Activo = dto.Activo;
        existing.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }

    private static EvaluacionNacionalDto MapToDto(EvaluacionNacional entity)
        => new(
            entity.Id,
            entity.EstudianteId,
            entity.AsignaturaId,
            entity.SeccionId,
            entity.PeriodoId,
            entity.NotaMinima,
            entity.NotaObtenida,
            entity.NotaPruebaEstandarizada,
            entity.AusentismoTardias,
            entity.AusentismoInjustificadas,
            entity.AusentismoJustificadas,
            entity.Observaciones,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.Condicion,
            entity.Aprobado,
            entity.TotalAusentismo,
            entity.Estudiante?.NombreCompleto,
            entity.Asignatura?.Nombre);

    private static EvaluacionNacional MapToEntity(EvaluacionNacionalDto dto)
        => new()
        {
            Id = dto.Id,
            EstudianteId = dto.EstudianteId,
            AsignaturaId = dto.AsignaturaId,
            SeccionId = dto.SeccionId,
            PeriodoId = dto.PeriodoId,
            NotaMinima = dto.NotaMinima,
            NotaObtenida = dto.NotaObtenida,
            NotaPruebaEstandarizada = dto.NotaPruebaEstandarizada,
            AusentismoTardias = dto.AusentismoTardias,
            AusentismoInjustificadas = dto.AusentismoInjustificadas,
            AusentismoJustificadas = dto.AusentismoJustificadas,
            Observaciones = dto.Observaciones,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}


