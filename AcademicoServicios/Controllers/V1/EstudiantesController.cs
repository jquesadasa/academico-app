using AcademicoServicios.Contracts.V1;
using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoServicios.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly IEstudianteService _service;

    public EstudiantesController(IEstudianteService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<EstudianteDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(EstudianteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _service.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("cedula/{cedula}")]
    [ProducesResponseType(typeof(EstudianteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCedula(string cedula, CancellationToken cancellationToken)
    {
        var item = await _service.GetByCedulaAsync(cedula, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("activos")]
    [ProducesResponseType(typeof(IReadOnlyList<EstudianteDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivos(CancellationToken cancellationToken)
        => Ok(await _service.GetActivosAsync(cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(EstudianteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] EstudianteUpsertRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Cedula))
        {
            return BadRequest("La cÃ©dula es requerida.");
        }

        var dto = new EstudianteDto(
            Guid.Empty,
            request.Cedula,
            request.Nombre,
            request.PrimerApellido,
            request.SegundoApellido,
            request.NumeroLista,
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
    public async Task<IActionResult> Update(Guid id, [FromBody] EstudianteUpsertRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Cedula))
        {
            return BadRequest("La cÃ©dula es requerida.");
        }

        var dto = new EstudianteDto(
            id,
            request.Cedula,
            request.Nombre,
            request.PrimerApellido,
            request.SegundoApellido,
            request.NumeroLista,
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

