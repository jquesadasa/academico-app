using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IDireccionRegionalService : IApplicationService<DireccionRegionalDto>
{
    Task<IReadOnlyList<DireccionRegionalDto>> GetActivasAsync(CancellationToken cancellationToken = default);
}
