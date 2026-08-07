using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class SeccionService : ISeccionService
{
    private readonly ISeccionRepository _repository;

    public SeccionService(ISeccionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<SeccionDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(MapToDto).ToList();

    public async Task<SeccionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IReadOnlyList<SeccionDto>> GetByPeriodoAsync(Guid periodoId, CancellationToken cancellationToken = default)
        => (await _repository.GetByPeriodoAsync(periodoId, cancellationToken)).Select(MapToDto).ToList();

    public async Task<SeccionDto?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByCodigoAsync(codigo, cancellationToken);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<SeccionDto> CreateAsync(SeccionDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.Id = Guid.Empty;

        var created = await _repository.AddAsync(entity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, SeccionDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.Codigo = dto.Codigo;
        existing.Nombre = dto.Nombre;
        existing.PeriodoId = dto.PeriodoId;
        existing.NivelId = dto.NivelId;
        existing.ProgramaAcademicoId = dto.ProgramaAcademicoId;
        existing.InstitucionId = dto.InstitucionId;
        existing.ProfesorGuiaId = dto.ProfesorGuiaId;
        existing.Activo = dto.Activo;

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

    private static SeccionDto MapToDto(Seccion entity)
        => new(
            entity.Id,
            entity.Codigo,
            entity.Nombre,
            entity.PeriodoId,
            entity.NivelId,
            entity.ProgramaAcademicoId,
            entity.InstitucionId,
            entity.ProfesorGuiaId,
            entity.Activo,
            entity.CreatedAt);

    private static Seccion MapToEntity(SeccionDto dto)
        => new()
        {
            Id = dto.Id,
            Codigo = dto.Codigo,
            Nombre = dto.Nombre,
            PeriodoId = dto.PeriodoId,
            NivelId = dto.NivelId,
            ProgramaAcademicoId = dto.ProgramaAcademicoId,
            InstitucionId = dto.InstitucionId,
            ProfesorGuiaId = dto.ProfesorGuiaId,
            Activo = dto.Activo,
            CreatedAt = dto.CreatedAt
        };
}


