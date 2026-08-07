using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class InstitucionService : IInstitucionService
{
    private readonly IInstitucionRepository _repository;

    public InstitucionService(IInstitucionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<InstitucionDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<InstitucionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<InstitucionDto>> GetActivasAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetActivasAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<IReadOnlyList<InstitucionDto>> GetByDireccionRegionalAsync(Guid direccionRegionalId, CancellationToken cancellationToken = default)
        => (await _repository.GetByDireccionRegionalAsync(direccionRegionalId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<InstitucionDto> CreateAsync(InstitucionDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, InstitucionDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.Nombre = dto.Nombre;
        existing.Codigo = dto.Codigo;
        existing.DireccionRegionalId = dto.DireccionRegionalId;
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

    private static InstitucionDto MapToDto(Institucion entity)
        => new(
            entity.Id,
            entity.Nombre,
            entity.Codigo,
            entity.DireccionRegionalId,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt);

    private static Institucion MapToEntity(InstitucionDto dto)
        => new()
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            Codigo = dto.Codigo,
            DireccionRegionalId = dto.DireccionRegionalId,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}


