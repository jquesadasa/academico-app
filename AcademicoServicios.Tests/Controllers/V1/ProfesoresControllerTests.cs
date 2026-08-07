using AcademicoNegocio.Dtos;
using AcademicoServicios.Contracts.V1;
using AcademicoServicios.Controllers.V1;
using AcademicoServicios.Tests.Fakes;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoServicios.Tests.Controllers.V1;

public class ProfesoresControllerTests
{
    [Fact]
    public async Task GetById_CuandoExiste_RetornaOk()
    {
        var profesorId = Guid.NewGuid();
        var service = new FakeProfesorService([
            new ProfesorDto(profesorId, "Matematica", "Ana", "Lopez", null, true, DateTime.UtcNow, null, "Ana Lopez")
        ]);
        var controller = new ProfesoresController(service);

        var result = await controller.GetById(profesorId, CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<ProfesorDto>(ok.Value);
        Assert.Equal(profesorId, dto.Id);
    }

    [Fact]
    public async Task GetById_CuandoNoExiste_RetornaNotFound()
    {
        var service = new FakeProfesorService();
        var controller = new ProfesoresController(service);

        var result = await controller.GetById(Guid.NewGuid(), CancellationToken.None);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_EspecialidadVacia_RetornaBadRequest()
    {
        var service = new FakeProfesorService();
        var controller = new ProfesoresController(service);

        var payload = new ProfesorUpsertRequest("", "Ana", "Lopez", null, true);
        var result = await controller.Create(payload, CancellationToken.None);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Delete_CuandoExiste_RetornaOk()
    {
        var profesorId = Guid.NewGuid();
        var service = new FakeProfesorService([
            new ProfesorDto(profesorId, "Matematica", "Ana", "Lopez", null, true, DateTime.UtcNow, null, "Ana Lopez")
        ]);
        var controller = new ProfesoresController(service);

        var result = await controller.Delete(profesorId, CancellationToken.None);

        Assert.IsType<OkResult>(result);
    }
}