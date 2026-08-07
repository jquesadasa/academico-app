using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IAsignaturaService : IApplicationService<AsignaturaDto>
{
    Task<AsignaturaDto?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default);
}
