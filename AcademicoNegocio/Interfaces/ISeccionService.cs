using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface ISeccionService : IApplicationService<SeccionDto>
{
    Task<IReadOnlyList<SeccionDto>> GetByPeriodoAsync(Guid periodoId, CancellationToken cancellationToken = default);
    Task<SeccionDto?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default);
}

