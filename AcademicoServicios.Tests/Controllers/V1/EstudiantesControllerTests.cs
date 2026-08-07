using AcademicoNegocio.Dtos;
using AcademicoServicios.Contracts.V1;
using AcademicoServicios.Controllers.V1;
using AcademicoServicios.Tests.Fakes;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoServicios.Tests.Controllers.V1;

public class EstudiantesControllerTests
{
    [Fact]
    public async Task GetById_CuandoExiste_RetornaOk()
    {
        var estudianteId = Guid.NewGuid();
        var service = new FakeEstudianteService([
            new EstudianteDto(estudianteId, "1-1111-1111", "Ana", "Lopez", null, null, true, DateTime.UtcNow, null, "Ana Lopez")
        ]);
        var controller = new EstudiantesController(service);

        var result = await controller.GetById(estudianteId, CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<EstudianteDto>(ok.Value);
        Assert.Equal(estudianteId, dto.Id);
    }

    [Fact]
    public async Task GetById_CuandoNoExiste_RetornaNotFound()
    {
        var service = new FakeEstudianteService();
        var controller = new EstudiantesController(service);

        var result = await controller.GetById(Guid.NewGuid(), CancellationToken.None);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_CedulaVacia_RetornaBadRequest()
    {
        var service = new FakeEstudianteService();
        var controller = new EstudiantesController(service);

        var payload = new EstudianteUpsertRequest("", "Ana", "Lopez", null, null, true);
        var result = await controller.Create(payload, CancellationToken.None);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Delete_CuandoExiste_RetornaOk()
    {
        var estudianteId = Guid.NewGuid();
        var service = new FakeEstudianteService([
            new EstudianteDto(estudianteId, "1-1111-1111", "Ana", "Lopez", null, null, true, DateTime.UtcNow, null, "Ana Lopez")
        ]);
        var controller = new EstudiantesController(service);

        var result = await controller.Delete(estudianteId, CancellationToken.None);

        Assert.IsType<OkResult>(result);
    }
}
