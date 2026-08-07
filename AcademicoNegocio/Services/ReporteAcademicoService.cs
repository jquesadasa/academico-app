using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoNegocio.Services;

public sealed class ReporteAcademicoService : IReporteAcademicoService
{
    private readonly ISeccionRepository _secciones;
    private readonly IPeriodoRepository _periodos;
    private readonly IMatriculaRepository _matriculas;
    private readonly IMonografiaRepository _monografias;
    private readonly ITeoriaConocimientoRepository _teoriaConocimiento;
    private readonly IEvaluacionBIRepository _evaluacionesBi;
    private readonly IEvaluacionNacionalRepository _evaluacionesNacionales;

    public ReporteAcademicoService(
        ISeccionRepository secciones,
        IPeriodoRepository periodos,
        IMatriculaRepository matriculas,
        IMonografiaRepository monografias,
        ITeoriaConocimientoRepository teoriaConocimiento,
        IEvaluacionBIRepository evaluacionesBi,
        IEvaluacionNacionalRepository evaluacionesNacionales)
    {
        _secciones = secciones;
        _periodos = periodos;
        _matriculas = matriculas;
        _monografias = monografias;
        _teoriaConocimiento = teoriaConocimiento;
        _evaluacionesBi = evaluacionesBi;
        _evaluacionesNacionales = evaluacionesNacionales;
    }

    public async Task<ReporteConsolidadoDto> GetConsolidadoAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default)
    {
        // Temporary compatibility shim while migrating report dependencies to Guid IDs.
        await Task.CompletedTask;
        return new ReporteConsolidadoDto(
            seccionId,
            string.Empty,
            periodoId,
            string.Empty,
            DateTime.UtcNow,
            []);
    }
}

