using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;
using AcademicoServicios.Contracts.V1;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoServicios.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MonografiasController : ControllerBase
{
    private readonly IMonografiaService _service;

    public MonografiasController(IMonografiaService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MonografiaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MonografiaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _service.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("estudiante/{estudianteId:guid}/periodo/{periodoId:guid}")]
    [ProducesResponseType(typeof(MonografiaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEstudianteYPeriodo(Guid estudianteId, Guid periodoId, CancellationToken cancellationToken)
    {
        var item = await _service.GetByEstudianteYPeriodoAsync(estudianteId, periodoId, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("seccion/{seccionId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<MonografiaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySeccion(Guid seccionId, CancellationToken cancellationToken)
        => Ok(await _service.GetBySeccionAsync(seccionId, cancellationToken));

    [HttpGet("supervisor/{supervisorId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<MonografiaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySupervisor(Guid supervisorId, CancellationToken cancellationToken)
        => Ok(await _service.GetBySupervisorAsync(supervisorId, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(MonografiaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] MonografiaUpsertRequest request, CancellationToken cancellationToken)
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
    public async Task<IActionResult> Update(Guid id, [FromBody] MonografiaUpsertRequest request, CancellationToken cancellationToken)
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

    private static string? ValidateRequest(MonografiaUpsertRequest request)
    {
        if (request.EstudianteId == Guid.Empty || request.SeccionId == Guid.Empty || request.PeriodoId == Guid.Empty)
        {
            return "Estudiante, secciÃ³n y perÃ­odo son requeridos.";
        }

        return null;
    }

    private static MonografiaDto ToDto(Guid id, MonografiaUpsertRequest request)
        => new(
            id,
            request.EstudianteId,
            request.SeccionId,
            request.PeriodoId,
            request.AreaInvestigacion,
            request.SupervisorNombre,
            request.SupervisorId,
            request.BandaAlcanzada,
            request.Observaciones,
            request.Activo,
            DateTime.UtcNow,
            null,
            null);
}





