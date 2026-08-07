using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface ITeoriaConocimientoService : IApplicationService<TeoriaConocimientoDto>
{
    Task<TeoriaConocimientoDto?> GetByEstudianteYPeriodoAsync(Guid estudianteId, Guid periodoId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TeoriaConocimientoDto>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TeoriaConocimientoDto>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
}

