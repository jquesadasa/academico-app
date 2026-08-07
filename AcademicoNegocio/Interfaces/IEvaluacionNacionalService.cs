using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IEvaluacionNacionalService : IApplicationService<EvaluacionNacionalDto>
{
    Task<IReadOnlyList<EvaluacionNacionalDto>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionNacionalDto>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionNacionalDto>> GetBySeccionYPeriodoAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
    Task<EvaluacionNacionalDto?> GetByEstudianteAsignaturaSeccionAsync(Guid estudianteId, Guid asignaturaId, Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EvaluacionNacionalDto>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
}

