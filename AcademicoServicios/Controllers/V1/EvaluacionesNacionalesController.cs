using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;
using AcademicoServicios.Contracts.V1;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoServicios.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class EvaluacionesNacionalesController : ControllerBase
{
    private readonly IEvaluacionNacionalService _service;

    public EvaluacionesNacionalesController(IEvaluacionNacionalService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<EvaluacionNacionalDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(EvaluacionNacionalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _service.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("estudiante/{estudianteId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<EvaluacionNacionalDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEstudiante(Guid estudianteId, CancellationToken cancellationToken)
        => Ok(await _service.GetByEstudianteAsync(estudianteId, cancellationToken));

    [HttpGet("seccion/{seccionId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<EvaluacionNacionalDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySeccion(Guid seccionId, CancellationToken cancellationToken)
        => Ok(await _service.GetBySeccionAsync(seccionId, cancellationToken));

    [HttpGet("seccion/{seccionId:guid}/periodo/{periodoId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<EvaluacionNacionalDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySeccionYPeriodo(Guid seccionId, Guid periodoId, CancellationToken cancellationToken)
        => Ok(await _service.GetBySeccionYPeriodoAsync(seccionId, periodoId, cancellationToken));

    [HttpGet("estudiante/{estudianteId:guid}/asignatura/{asignaturaId:guid}/seccion/{seccionId:guid}")]
    [ProducesResponseType(typeof(EvaluacionNacionalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEstudianteAsignaturaSeccion(Guid estudianteId, Guid asignaturaId, Guid seccionId, CancellationToken cancellationToken)
    {
        var item = await _service.GetByEstudianteAsignaturaSeccionAsync(estudianteId, asignaturaId, seccionId, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("consolidado/seccion/{seccionId:guid}/periodo/{periodoId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<EvaluacionNacionalDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConsolidadoGrupal(Guid seccionId, Guid periodoId, CancellationToken cancellationToken)
        => Ok(await _service.GetConsolidadoGrupalAsync(seccionId, periodoId, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(EvaluacionNacionalDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] EvaluacionNacionalUpsertRequest request, CancellationToken cancellationToken)
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
    public async Task<IActionResult> Update(Guid id, [FromBody] EvaluacionNacionalUpsertRequest request, CancellationToken cancellationToken)
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

    private static string? ValidateRequest(EvaluacionNacionalUpsertRequest request)
    {
        if (request.EstudianteId == Guid.Empty || request.AsignaturaId == Guid.Empty || request.SeccionId == Guid.Empty || request.PeriodoId == Guid.Empty)
        {
            return "Estudiante, asignatura, secciÃ³n y perÃ­odo son requeridos.";
        }

        if (request.NotaMinima < 0 || request.NotaMinima > 100)
        {
            return "La nota mÃ­nima debe estar entre 0 y 100.";
        }

        if (request.NotaObtenida.HasValue && (request.NotaObtenida < 0 || request.NotaObtenida > 100))
        {
            return "La nota obtenida debe estar entre 0 y 100.";
        }

        return null;
    }

    private static EvaluacionNacionalDto ToDto(Guid id, EvaluacionNacionalUpsertRequest request)
        => new(
            id,
            request.EstudianteId,
            request.AsignaturaId,
            request.SeccionId,
            request.PeriodoId,
            request.NotaMinima,
            request.NotaObtenida,
            request.NotaPruebaEstandarizada,
            request.AusentismoTardias,
            request.AusentismoInjustificadas,
            request.AusentismoJustificadas,
            request.Observaciones,
            request.Activo,
            DateTime.UtcNow,
            null,
            string.Empty,
            false,
            0,
            null,
            null);
}





