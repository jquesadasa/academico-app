using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class DireccionRegionalService : IDireccionRegionalService
{
    private readonly IDireccionRegionalRepository _repository;

    public DireccionRegionalService(IDireccionRegionalRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<DireccionRegionalDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<DireccionRegionalDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<DireccionRegionalDto>> GetActivasAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetActivasAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<DireccionRegionalDto> CreateAsync(DireccionRegionalDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, DireccionRegionalDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.Nombre = dto.Nombre;
        existing.Codigo = dto.Codigo;
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

    private static DireccionRegionalDto MapToDto(DireccionRegional entity)
        => new(
            entity.Id,
            entity.Nombre,
            entity.Codigo,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt);

    private static DireccionRegional MapToEntity(DireccionRegionalDto dto)
        => new()
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            Codigo = dto.Codigo,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}

