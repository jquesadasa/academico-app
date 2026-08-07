using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IEstudianteService : IApplicationService<EstudianteDto>
{
    Task<EstudianteDto?> GetByCedulaAsync(string cedula, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EstudianteDto>> GetActivosAsync(CancellationToken cancellationToken = default);
}
