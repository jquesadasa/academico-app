using AcademicoServicios.Contracts.V1;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoServicios.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly IProfesorService _service;

    public ProfesoresController(IProfesorService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProfesorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProfesorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _service.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("activos")]
    [ProducesResponseType(typeof(IReadOnlyList<ProfesorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivos(CancellationToken cancellationToken)
        => Ok(await _service.GetActivosAsync(cancellationToken));

    [HttpGet("especialidad/{especialidad}")]
    [ProducesResponseType(typeof(IReadOnlyList<ProfesorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEspecialidad(string especialidad, CancellationToken cancellationToken)
        => Ok(await _service.GetByEspecialidadAsync(especialidad, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(ProfesorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ProfesorUpsertRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Especialidad))
        {
            return BadRequest("La especialidad es requerida.");
        }

        var dto = new ProfesorDto(
            Guid.Empty,
            request.Especialidad,
            request.Nombre,
            request.PrimerApellido,
            request.SegundoApellido,
            request.Activo,
            DateTime.UtcNow,
            null,
            string.Empty);

        var created = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1" }, created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProfesorUpsertRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Especialidad))
        {
            return BadRequest("La especialidad es requerida.");
        }

        var dto = new ProfesorDto(
            id,
            request.Especialidad,
            request.Nombre,
            request.PrimerApellido,
            request.SegundoApellido,
            request.Activo,
            DateTime.UtcNow,
            null,
            string.Empty);

        var updated = await _service.UpdateAsync(id, dto, cancellationToken);
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
}


