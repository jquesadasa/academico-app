using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IEvaluacionBIService : IApplicationService<EvaluacionBIDto>
{
    Task<IReadOnlyList<EvaluacionBIDto>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionBIDto>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionBIDto>> GetBySeccionYPeriodoAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
    Task<EvaluacionBIDto?> GetByEstudianteAsignaturaSeccionAsync(Guid estudianteId, Guid asignaturaId, Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionBIDto>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
}

