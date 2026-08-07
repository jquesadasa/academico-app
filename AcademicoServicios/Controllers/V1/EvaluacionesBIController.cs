using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;
using AcademicoServicios.Contracts.V1;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoServicios.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class EvaluacionesBIController : ControllerBase
{
    private readonly IEvaluacionBIService _service;

    public EvaluacionesBIController(IEvaluacionBIService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<EvaluacionBIDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(EvaluacionBIDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _service.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("estudiante/{estudianteId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<EvaluacionBIDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEstudiante(Guid estudianteId, CancellationToken cancellationToken)
        => Ok(await _service.GetByEstudianteAsync(estudianteId, cancellationToken));

    [HttpGet("seccion/{seccionId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<EvaluacionBIDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySeccion(Guid seccionId, CancellationToken cancellationToken)
        => Ok(await _service.GetBySeccionAsync(seccionId, cancellationToken));

    [HttpGet("seccion/{seccionId:guid}/periodo/{periodoId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<EvaluacionBIDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySeccionYPeriodo(Guid seccionId, Guid periodoId, CancellationToken cancellationToken)
        => Ok(await _service.GetBySeccionYPeriodoAsync(seccionId, periodoId, cancellationToken));

    [HttpGet("estudiante/{estudianteId:guid}/asignatura/{asignaturaId:guid}/seccion/{seccionId:guid}")]
    [ProducesResponseType(typeof(EvaluacionBIDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEstudianteAsignaturaSeccion(Guid estudianteId, Guid asignaturaId, Guid seccionId, CancellationToken cancellationToken)
    {
        var item = await _service.GetByEstudianteAsignaturaSeccionAsync(estudianteId, asignaturaId, seccionId, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("consolidado/seccion/{seccionId:guid}/periodo/{periodoId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<EvaluacionBIDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConsolidadoGrupal(Guid seccionId, Guid periodoId, CancellationToken cancellationToken)
        => Ok(await _service.GetConsolidadoGrupalAsync(seccionId, periodoId, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(EvaluacionBIDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] EvaluacionBIUpsertRequest request, CancellationToken cancellationToken)
    {
        var validationError = ValidateRequest(request);
        if (validationError is not null)
        {
            return BadRequest(validationError);
        }

        var dto = ToDto(Guid.Empty, request);
        var created = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1" }, created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] EvaluacionBIUpsertRequest request, CancellationToken cancellationToken)
    {
        var validationError = ValidateRequest(request);
        if (validationError is not null)
        {
            return BadRequest(validationError);
        }

        var updated = await _service.UpdateAsync(id, ToDto(id, request), cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _service.DeleteAsync(id, cancellationToken);
        return deleted ? Ok() : NotFound();
    }

    private static string? ValidateRequest(EvaluacionBIUpsertRequest request)
    {
        if (request.EstudianteId == Guid.Empty || request.AsignaturaId == Guid.Empty || request.SeccionId == Guid.Empty || request.PeriodoId == Guid.Empty)
        {
            return "Estudiante, asignatura, secciÃ³n y perÃ­odo son requeridos.";
        }

        if (request.BandaMinima < 1 || request.BandaMinima > 7)
        {
            return "La banda mÃ­nima debe estar entre 1 y 7.";
        }

        if (request.BandaAlcanzada.HasValue && (request.BandaAlcanzada < 1 || request.BandaAlcanzada > 7))
        {
            return "La banda alcanzada debe estar entre 1 y 7.";
        }

        return null;
    }

    private static EvaluacionBIDto ToDto(Guid id, EvaluacionBIUpsertRequest request)
        => new(
            id,
            request.EstudianteId,
            request.AsignaturaId,
            request.SeccionId,
            request.PeriodoId,
            request.BandaMinima,
            request.BandaAlcanzada,
            request.AusentismoTardias,
            request.AusentismoInjustificadas,
            request.AusentismoJustificadas,
            request.Observaciones,
            request.Activo,
            DateTime.UtcNow,
            null,
            false,
            0,
            null,
            null);
}





