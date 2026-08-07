using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class NivelService : INivelService
{
    private readonly INivelRepository _repository;

    public NivelService(INivelRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<NivelDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<NivelDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<NivelDto>> GetActivosOrdenadosAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetActivosOrdenadosAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<NivelDto> CreateAsync(NivelDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, NivelDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.Nombre = dto.Nombre;
        existing.Orden = dto.Orden;
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

    private static NivelDto MapToDto(Nivel entity)
        => new(
            entity.Id,
            entity.Nombre,
            entity.Orden,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt);

    private static Nivel MapToEntity(NivelDto dto)
        => new()
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            Orden = dto.Orden,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}

