using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class MonografiaService : IMonografiaService
{
    private readonly IMonografiaRepository _repository;

    public MonografiaService(IMonografiaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<MonografiaDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<MonografiaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<MonografiaDto?> GetByEstudianteYPeriodoAsync(Guid estudianteId, Guid periodoId, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByEstudianteYPeriodoAsync(estudianteId, periodoId, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<MonografiaDto>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default)
        => (await _repository.GetBySeccionAsync(seccionId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<IReadOnlyList<MonografiaDto>> GetBySupervisorAsync(Guid supervisorId, CancellationToken cancellationToken = default)
        => (await _repository.GetBySupervisorAsync(supervisorId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<MonografiaDto> CreateAsync(MonografiaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, MonografiaDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.EstudianteId = dto.EstudianteId;
        existing.SeccionId = dto.SeccionId;
        existing.PeriodoId = dto.PeriodoId;
        existing.AreaInvestigacion = dto.AreaInvestigacion;
        existing.SupervisorNombre = dto.SupervisorNombre;
        existing.SupervisorId = dto.SupervisorId;
        existing.BandaAlcanzada = dto.BandaAlcanzada;
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

    private static MonografiaDto MapToDto(Monografia entity)
        => new(
            entity.Id,
            entity.EstudianteId,
            entity.SeccionId,
            entity.PeriodoId,
            entity.AreaInvestigacion,
            entity.SupervisorNombre,
            entity.SupervisorId,
            entity.BandaAlcanzada,
            entity.Observaciones,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.Estudiante?.NombreCompleto);

    private static Monografia MapToEntity(MonografiaDto dto)
        => new()
        {
            Id = dto.Id,
            EstudianteId = dto.EstudianteId,
            SeccionId = dto.SeccionId,
            PeriodoId = dto.PeriodoId,
            AreaInvestigacion = dto.AreaInvestigacion,
            SupervisorNombre = dto.SupervisorNombre,
            SupervisorId = dto.SupervisorId,
            BandaAlcanzada = dto.BandaAlcanzada,
            Observaciones = dto.Observaciones,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}


