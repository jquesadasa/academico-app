using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class TeoriaConocimientoService : ITeoriaConocimientoService
{
    private readonly ITeoriaConocimientoRepository _repository;

    public TeoriaConocimientoService(ITeoriaConocimientoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TeoriaConocimientoDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<TeoriaConocimientoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<TeoriaConocimientoDto?> GetByEstudianteYPeriodoAsync(Guid estudianteId, Guid periodoId, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByEstudianteYPeriodoAsync(estudianteId, periodoId, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<TeoriaConocimientoDto>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default)
        => (await _repository.GetBySeccionAsync(seccionId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<IReadOnlyList<TeoriaConocimientoDto>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default)
        => (await _repository.GetConsolidadoGrupalAsync(seccionId, periodoId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<TeoriaConocimientoDto> CreateAsync(TeoriaConocimientoDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, TeoriaConocimientoDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.EstudianteId = dto.EstudianteId;
        existing.SeccionId = dto.SeccionId;
        existing.PeriodoId = dto.PeriodoId;
        existing.BandaAlcanzada = dto.BandaAlcanzada;
        existing.AusentismoExhibicion = dto.AusentismoExhibicion;
        existing.AusentismoOralidad = dto.AusentismoOralidad;
        existing.ObservacionesExhibicion = dto.ObservacionesExhibicion;
        existing.ObservacionesArgumentos = dto.ObservacionesArgumentos;
        existing.ObservacionesOralidad = dto.ObservacionesOralidad;
        existing.ObservacionesEscritura = dto.ObservacionesEscritura;
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

    private static TeoriaConocimientoDto MapToDto(TeoriaConocimiento entity)
        => new(
            entity.Id,
            entity.EstudianteId,
            entity.SeccionId,
            entity.PeriodoId,
            entity.BandaAlcanzada,
            entity.AusentismoExhibicion,
            entity.AusentismoOralidad,
            entity.ObservacionesExhibicion,
            entity.ObservacionesArgumentos,
            entity.ObservacionesOralidad,
            entity.ObservacionesEscritura,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.Estudiante?.NombreCompleto);

    private static TeoriaConocimiento MapToEntity(TeoriaConocimientoDto dto)
        => new()
        {
            Id = dto.Id,
            EstudianteId = dto.EstudianteId,
            SeccionId = dto.SeccionId,
            PeriodoId = dto.PeriodoId,
            BandaAlcanzada = dto.BandaAlcanzada,
            AusentismoExhibicion = dto.AusentismoExhibicion,
            AusentismoOralidad = dto.AusentismoOralidad,
            ObservacionesExhibicion = dto.ObservacionesExhibicion,
            ObservacionesArgumentos = dto.ObservacionesArgumentos,
            ObservacionesOralidad = dto.ObservacionesOralidad,
            ObservacionesEscritura = dto.ObservacionesEscritura,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}


