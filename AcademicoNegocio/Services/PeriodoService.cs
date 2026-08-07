using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class PeriodoService : IPeriodoService
{
    private readonly IPeriodoRepository _repository;

    public PeriodoService(IPeriodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PeriodoDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<PeriodoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<PeriodoDto>> GetActivosAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetActivosAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<IReadOnlyList<PeriodoDto>> GetByAnioAsync(int anio, CancellationToken cancellationToken = default)
        => (await _repository.GetByAnioAsync(anio, cancellationToken)).Select(MapToDto).ToList();

    public async Task<PeriodoDto?> GetVigenteAsync(CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetVigenteAsync(cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<PeriodoDto> CreateAsync(PeriodoDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, PeriodoDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.Nombre = dto.Nombre;
        existing.FechaInicio = dto.FechaInicio;
        existing.FechaFin = dto.FechaFin;
        existing.Anio = dto.Anio;
        existing.Estado = dto.Estado;
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

    private static PeriodoDto MapToDto(Periodo entity)
        => new(
            entity.Id,
            entity.Nombre,
            entity.FechaInicio,
            entity.FechaFin,
            entity.Anio,
            entity.Estado,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt);

    private static Periodo MapToEntity(PeriodoDto dto)
        => new()
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            FechaInicio = dto.FechaInicio,
            FechaFin = dto.FechaFin,
            Anio = dto.Anio,
            Estado = dto.Estado,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}

