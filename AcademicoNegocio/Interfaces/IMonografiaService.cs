using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IMonografiaService : IApplicationService<MonografiaDto>
{
    Task<MonografiaDto?> GetByEstudianteYPeriodoAsync(Guid estudianteId, Guid periodoId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MonografiaDto>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MonografiaDto>> GetBySupervisorAsync(Guid supervisorId, CancellationToken cancellationToken = default);
}

