using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IMonografiaRepository : IRepository<Monografia>
{
    Task<Monografia?> GetByEstudianteYPeriodoAsync(Guid estudianteId, Guid periodoId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Monografia>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Monografia>> GetBySupervisorAsync(Guid supervisorId, CancellationToken cancellationToken = default);
}

