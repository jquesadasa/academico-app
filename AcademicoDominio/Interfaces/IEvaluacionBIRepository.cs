using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IEvaluacionBIRepository : IRepository<EvaluacionBI>
{
    Task<IReadOnlyList<EvaluacionBI>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionBI>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionBI>> GetBySeccionYPeriodoAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
    Task<EvaluacionBI?> GetByEstudianteAsignaturaSeccionAsync(Guid estudianteId, Guid asignaturaId, Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionBI>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
}

