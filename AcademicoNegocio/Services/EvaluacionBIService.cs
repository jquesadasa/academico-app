using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class EvaluacionBIService : IEvaluacionBIService
{
    private readonly IEvaluacionBIRepository _repository;

    public EvaluacionBIService(IEvaluacionBIRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EvaluacionBIDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<EvaluacionBIDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<EvaluacionBIDto>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default)
        => (await _repository.GetByEstudianteAsync(estudianteId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<IReadOnlyList<EvaluacionBIDto>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default)
        => (await _repository.GetBySeccionAsync(seccionId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<IReadOnlyList<EvaluacionBIDto>> GetBySeccionYPeriodoAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default)
        => (await _repository.GetBySeccionYPeriodoAsync(seccionId, periodoId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<EvaluacionBIDto?> GetByEstudianteAsignaturaSeccionAsync(Guid estudianteId, Guid asignaturaId, Guid seccionId, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByEstudianteAsignaturaSeccionAsync(estudianteId, asignaturaId, seccionId, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<EvaluacionBIDto>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default)
        => (await _repository.GetConsolidadoGrupalAsync(seccionId, periodoId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<EvaluacionBIDto> CreateAsync(EvaluacionBIDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, EvaluacionBIDto dto, CancellationToken cancellationToken = default)
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
        existing.BandaMinima = dto.BandaMinima;
        existing.BandaAlcanzada = dto.BandaAlcanzada;
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

    private static EvaluacionBIDto MapToDto(EvaluacionBI entity)
        => new(
            entity.Id,
            entity.EstudianteId,
            entity.AsignaturaId,
            entity.SeccionId,
            entity.PeriodoId,
            entity.BandaMinima,
            entity.BandaAlcanzada,
            entity.AusentismoTardias,
            entity.AusentismoInjustificadas,
            entity.AusentismoJustificadas,
            entity.Observaciones,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.Aprobado,
            entity.TotalAusentismo,
            entity.Estudiante?.NombreCompleto,
            entity.Asignatura?.Nombre);

    private static EvaluacionBI MapToEntity(EvaluacionBIDto dto)
        => new()
        {
            Id = dto.Id,
            EstudianteId = dto.EstudianteId,
            AsignaturaId = dto.AsignaturaId,
            SeccionId = dto.SeccionId,
            PeriodoId = dto.PeriodoId,
            BandaMinima = dto.BandaMinima,
            BandaAlcanzada = dto.BandaAlcanzada,
            AusentismoTardias = dto.AusentismoTardias,
            AusentismoInjustificadas = dto.AusentismoInjustificadas,
            AusentismoJustificadas = dto.AusentismoJustificadas,
            Observaciones = dto.Observaciones,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}


