using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IProfesorService : IApplicationService<ProfesorDto>
{
    Task<IReadOnlyList<ProfesorDto>> GetActivosAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProfesorDto>> GetByEspecialidadAsync(string especialidad, CancellationToken cancellationToken = default);
}
