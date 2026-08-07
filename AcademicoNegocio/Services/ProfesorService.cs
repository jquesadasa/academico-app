using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class ProfesorService : IProfesorService
{
    private readonly IProfesorRepository _repository;

    public ProfesorService(IProfesorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ProfesorDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<ProfesorDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<ProfesorDto>> GetActivosAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetActivosAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<IReadOnlyList<ProfesorDto>> GetByEspecialidadAsync(string especialidad, CancellationToken cancellationToken = default)
        => (await _repository.GetByEspecialidadAsync(especialidad, cancellationToken)).Select(MapToDto).ToList();

    public async Task<ProfesorDto> CreateAsync(ProfesorDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, ProfesorDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.Especialidad = dto.Especialidad;
        existing.Nombre = dto.Nombre;
        existing.PrimerApellido = dto.PrimerApellido;
        existing.SegundoApellido = dto.SegundoApellido;
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

    private static ProfesorDto MapToDto(Profesor entity)
        => new(
            entity.Id,
            entity.Especialidad,
            entity.Nombre,
            entity.PrimerApellido,
            entity.SegundoApellido,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.NombreCompleto);

    private static Profesor MapToEntity(ProfesorDto dto)
        => new()
        {
            Id = dto.Id,
            Especialidad = dto.Especialidad,
            Nombre = dto.Nombre,
            PrimerApellido = dto.PrimerApellido,
            SegundoApellido = dto.SegundoApellido,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
}

