using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AcademicoServicios.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReportesAcademicosController : ControllerBase
{
    private readonly IReporteAcademicoService _service;

    public ReportesAcademicosController(IReporteAcademicoService service)
    {
        _service = service;
    }

    [HttpGet("consolidado/seccion/{seccionId:guid}/periodo/{periodoId:guid}")]
    [ProducesResponseType(typeof(ReporteConsolidadoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetConsolidado(Guid seccionId, Guid periodoId, CancellationToken cancellationToken)
    {
        if (seccionId == Guid.Empty || periodoId == Guid.Empty)
        {
            return BadRequest("SecciÃ³n y perÃ­odo son requeridos.");
        }

        return Ok(await _service.GetConsolidadoAsync(seccionId, periodoId, cancellationToken));
    }

    [HttpGet("consolidado/seccion/{seccionId:guid}/periodo/{periodoId:guid}/csv")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExportarConsolidadoCsv(Guid seccionId, Guid periodoId, CancellationToken cancellationToken)
    {
        if (seccionId == Guid.Empty || periodoId == Guid.Empty)
        {
            return BadRequest("SecciÃ³n y perÃ­odo son requeridos.");
        }

        var consolidado = await _service.GetConsolidadoAsync(seccionId, periodoId, cancellationToken);
        var csv = BuildCsv(consolidado);
        var bytes = Encoding.UTF8.GetBytes(csv);
        var fileName = $"reporte-bandas-seccion-{seccionId}-periodo-{periodoId}.csv";

        return File(bytes, "text/csv", fileName);
    }

    private static string BuildCsv(ReporteConsolidadoDto consolidado)
    {
        var sb = new StringBuilder();
        sb.AppendLine("NumeroLista,Cedula,NombreCompleto,Componente,Asignatura,BandaMinima,BandaAlcanzada,NotaMinima,NotaObtenida,NotaPruebaEstandarizada,AusTardias,AusInjustificadas,AusJustificadas,Condicion,Aprobado,Observaciones");

        foreach (var estudiante in consolidado.Estudiantes)
        {
            if (estudiante.Monografia is not null)
            {
                sb.AppendLine(string.Join(',',
                    Csv(estudiante.NumeroLista),
                    Csv(estudiante.Cedula),
                    Csv(estudiante.NombreCompleto),
                    Csv("Monografia"),
                    Csv(estudiante.Monografia.AreaInvestigacion),
                    Csv(string.Empty),
                    Csv(estudiante.Monografia.BandaAlcanzada),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(estudiante.Monografia.Observaciones)));
            }

            if (estudiante.TeoriaConocimiento is not null)
            {
                sb.AppendLine(string.Join(',',
                    Csv(estudiante.NumeroLista),
                    Csv(estudiante.Cedula),
                    Csv(estudiante.NombreCompleto),
                    Csv("TeoriaConocimiento"),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(estudiante.TeoriaConocimiento.BandaAlcanzada),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(estudiante.TeoriaConocimiento.AusentismoExhibicion),
                    Csv(estudiante.TeoriaConocimiento.AusentismoOralidad),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(string.Join(" | ", new[]
                    {
                        estudiante.TeoriaConocimiento.ObservacionesExhibicion,
                        estudiante.TeoriaConocimiento.ObservacionesArgumentos,
                        estudiante.TeoriaConocimiento.ObservacionesOralidad,
                        estudiante.TeoriaConocimiento.ObservacionesEscritura
                    }.Where(x => !string.IsNullOrWhiteSpace(x)))))
                );
            }

            foreach (var bi in estudiante.EvaluacionesBI)
            {
                sb.AppendLine(string.Join(',',
                    Csv(estudiante.NumeroLista),
                    Csv(estudiante.Cedula),
                    Csv(estudiante.NombreCompleto),
                    Csv("EvaluacionBI"),
                    Csv(bi.Asignatura),
                    Csv(bi.BandaMinima),
                    Csv(bi.BandaAlcanzada),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(bi.AusentismoTardias),
                    Csv(bi.AusentismoInjustificadas),
                    Csv(bi.AusentismoJustificadas),
                    Csv(string.Empty),
                    Csv(bi.Aprobado ? "Aprobado" : "Reprobado"),
                    Csv(bi.Observaciones)));
            }

            foreach (var nacional in estudiante.EvaluacionesNacionales)
            {
                sb.AppendLine(string.Join(',',
                    Csv(estudiante.NumeroLista),
                    Csv(estudiante.Cedula),
                    Csv(estudiante.NombreCompleto),
                    Csv("EvaluacionNacional"),
                    Csv(nacional.Asignatura),
                    Csv(string.Empty),
                    Csv(string.Empty),
                    Csv(nacional.NotaMinima),
                    Csv(nacional.NotaObtenida),
                    Csv(nacional.NotaPruebaEstandarizada),
                    Csv(nacional.AusentismoTardias),
                    Csv(nacional.AusentismoInjustificadas),
                    Csv(nacional.AusentismoJustificadas),
                    Csv(nacional.Condicion),
                    Csv(nacional.Aprobado ? "Aprobado" : "Reprobado"),
                    Csv(nacional.Observaciones)));
            }
        }

        return sb.ToString();
    }

    private static string Csv(object? value)
    {
        var text = value?.ToString() ?? string.Empty;
        text = text.Replace("\"", "\"\"");
        return $"\"{text}\"";
    }
}

