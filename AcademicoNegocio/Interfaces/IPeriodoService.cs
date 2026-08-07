using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IPeriodoService : IApplicationService<PeriodoDto>
{
    Task<IReadOnlyList<PeriodoDto>> GetActivosAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PeriodoDto>> GetByAnioAsync(int anio, CancellationToken cancellationToken = default);
    Task<PeriodoDto?> GetVigenteAsync(CancellationToken cancellationToken = default);
}
