using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class AsignaturaService : IAsignaturaService
{
    private readonly IAsignaturaRepository _repository;

    public AsignaturaService(IAsignaturaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<AsignaturaDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<AsignaturaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<AsignaturaDto?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByNombreAsync(nombre, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<AsignaturaDto> CreateAsync(AsignaturaDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, AsignaturaDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.Nombre = dto.Nombre;
        existing.TipoEvaluacion = dto.TipoEvaluacion;
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

    private static AsignaturaDto MapToDto(Asignatura entity)
        => new(
            entity.Id,
            entity.Nombre,
            entity.TipoEvaluacion,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.EsBI,
            entity.EsNacional);

    private static Asignatura MapToEntity(AsignaturaDto dto)
        => new()
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            TipoEvaluacion = dto.TipoEvaluacion,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}

