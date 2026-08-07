using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IInstitucionService : IApplicationService<InstitucionDto>
{
    Task<IReadOnlyList<InstitucionDto>> GetActivasAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InstitucionDto>> GetByDireccionRegionalAsync(Guid direccionRegionalId, CancellationToken cancellationToken = default);
}

