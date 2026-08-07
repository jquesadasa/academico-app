using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IReporteAcademicoService
{
    Task<ReporteConsolidadoDto> GetConsolidadoAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
}
