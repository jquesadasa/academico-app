using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IEvaluacionNacionalRepository : IRepository<EvaluacionNacional>
{
    Task<IReadOnlyList<EvaluacionNacional>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionNacional>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionNacional>> GetBySeccionYPeriodoAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
    Task<EvaluacionNacional?> GetByEstudianteAsignaturaSeccionAsync(Guid estudianteId, Guid asignaturaId, Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionNacional>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
}

