using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class EstudianteService : IEstudianteService
{
    private readonly IEstudianteRepository _repository;

    public EstudianteService(IEstudianteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EstudianteDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<EstudianteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<EstudianteDto?> GetByCedulaAsync(string cedula, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByCedulaAsync(cedula, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<EstudianteDto>> GetActivosAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetActivosAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<EstudianteDto> CreateAsync(EstudianteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, EstudianteDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.Cedula = dto.Cedula;
        existing.Nombre = dto.Nombre?.Trim();
        existing.PrimerApellido = dto.PrimerApellido?.Trim();
        existing.SegundoApellido = dto.SegundoApellido?.Trim();
        existing.NumeroLista = dto.NumeroLista;
        existing.Iniciales = BuildIniciales(existing.NombreCompleto);
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

    private static EstudianteDto MapToDto(Estudiante entity)
        => new(
            entity.Id,
            entity.Cedula,
            entity.Nombre,
            entity.PrimerApellido,
            entity.SegundoApellido,
            entity.NumeroLista,
            entity.Activo,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.NombreCompleto);

    private static Estudiante MapToEntity(EstudianteDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Cedula = dto.Cedula,
            Nombre = dto.Nombre?.Trim(),
            Iniciales = BuildIniciales(BuildNombreCompleto(dto)),
            PrimerApellido = dto.PrimerApellido?.Trim(),
            SegundoApellido = dto.SegundoApellido?.Trim(),
            NumeroLista = dto.NumeroLista,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
    }

    private static string BuildNombreCompleto(EstudianteDto dto)
    {
        var partes = new[] { dto.Nombre, dto.PrimerApellido, dto.SegundoApellido }
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x!.Trim())
            .ToList();

        if (partes.Count > 0)
        {
            return string.Join(" ", partes);
        }

        return dto.NombreCompleto?.Trim() ?? string.Empty;
    }

    private static string BuildIniciales(string? nombreCompleto)
    {
        if (string.IsNullOrWhiteSpace(nombreCompleto))
        {
            return string.Empty;
        }

        var partes = nombreCompleto
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => char.ToUpperInvariant(x[0]));

        return string.Concat(partes).Trim();
    }
}

