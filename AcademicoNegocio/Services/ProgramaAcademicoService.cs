using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class ProgramaAcademicoService : IProgramaAcademicoService
{
    private readonly IProgramaAcademicoRepository _repository;

    public ProgramaAcademicoService(IProgramaAcademicoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ProgramaAcademicoDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<ProgramaAcademicoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<ProgramaAcademicoDto>> GetActivosAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetActivosAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<ProgramaAcademicoDto> CreateAsync(ProgramaAcademicoDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, ProgramaAcademicoDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.Nombre = dto.Nombre;
        existing.Descripcion = dto.Descripcion;
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

    private static ProgramaAcademicoDto MapToDto(ProgramaAcademico entity)
        => new(
            entity.Id,
            entity.Nombre,
            entity.Descripcion,
            entity.Codigo,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt);

    private static ProgramaAcademico MapToEntity(ProgramaAcademicoDto dto)
        => new()
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Codigo = dto.Codigo,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}

