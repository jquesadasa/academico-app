using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface INivelService : IApplicationService<NivelDto>
{
    Task<IReadOnlyList<NivelDto>> GetActivosOrdenadosAsync(CancellationToken cancellationToken = default);
}
