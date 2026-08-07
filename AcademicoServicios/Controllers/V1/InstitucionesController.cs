using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoServicios.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class InstitucionesController : ControllerBase
{
    private readonly IInstitucionService _service;

    public InstitucionesController(IInstitucionService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<InstitucionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(InstitucionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await _service.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("activas")]
    [ProducesResponseType(typeof(IReadOnlyList<InstitucionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivas(CancellationToken cancellationToken)
        => Ok(await _service.GetActivasAsync(cancellationToken));

    [HttpGet("direccion-regional/{direccionRegionalId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<InstitucionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDireccionRegional(Guid direccionRegionalId, CancellationToken cancellationToken)
        => Ok(await _service.GetByDireccionRegionalAsync(direccionRegionalId, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(InstitucionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] InstitucionDto dto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
        {
            return BadRequest("El nombre es requerido.");
        }

        var created = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1" }, created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] InstitucionDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id)
        {
            return BadRequest("El id de ruta no coincide con el id del cuerpo.");
        }

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



