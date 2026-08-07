using AcademicoNegocio.Dtos;
using AcademicoNegocio.Services;
using AcademicoNegocio.Tests.Fakes;

namespace AcademicoNegocio.Tests.Services;

public class EstudianteServiceTests
{
    [Fact]
    public async Task CreateAsync_DtoValido_CreaYRetornaConId()
    {
        var repository = new FakeEstudianteRepository();
        var service = new EstudianteService(repository);

        var dto = new EstudianteDto(
            Guid.Empty,
            "1-1111-1111",
            "Ana",
            "Lopez",
            "Mora",
            5,
            true,
            DateTime.UtcNow,
            null,
            "");

        var created = await service.CreateAsync(dto);

        Assert.NotEqual(Guid.Empty, created.Id);
        Assert.Equal("1-1111-1111", created.Cedula);
        Assert.Equal("Ana", created.Nombre);
        Assert.Equal("Lopez", created.PrimerApellido);
        Assert.Equal("Mora", created.SegundoApellido);
    }

    [Fact]
    public async Task GetByCedulaAsync_CuandoExiste_RetornaRegistro()
    {
        var repository = new FakeEstudianteRepository();
        var service = new EstudianteService(repository);

        await service.CreateAsync(new EstudianteDto(Guid.Empty, "2-2222-2222", "Luis", "Perez", null, null, true, DateTime.UtcNow, null, ""));

        var result = await service.GetByCedulaAsync("2-2222-2222");

        Assert.NotNull(result);
        Assert.Equal("Luis", result!.Nombre);
        Assert.Equal("Perez", result.PrimerApellido);
    }

    [Fact]
    public async Task GetActivosAsync_RetornaSoloActivos()
    {
        var repository = new FakeEstudianteRepository();
        var service = new EstudianteService(repository);

        await service.CreateAsync(new EstudianteDto(Guid.Empty, "3-3333-3333", "Activo", "Uno", null, null, true, DateTime.UtcNow, null, ""));
        await service.CreateAsync(new EstudianteDto(Guid.Empty, "4-4444-4444", "Inactivo", "Dos", null, null, false, DateTime.UtcNow, null, ""));

        var activos = await service.GetActivosAsync();

        Assert.Single(activos);
        Assert.Equal("3-3333-3333", activos[0].Cedula);
    }

    [Fact]
    public async Task UpdateAsync_CuandoExiste_ActualizaYRetornaTrue()
    {
        var repository = new FakeEstudianteRepository();
        var service = new EstudianteService(repository);

        var created = await service.CreateAsync(new EstudianteDto(Guid.Empty, "5-5555-5555", "Mario", "Soto", null, null, true, DateTime.UtcNow, null, ""));
        var updated = await service.UpdateAsync(
            created.Id,
            created with { Nombre = "Mario Actualizado" });

        var current = await service.GetByIdAsync(created.Id);

        Assert.True(updated);
        Assert.NotNull(current);
        Assert.Equal("Mario Actualizado", current!.Nombre);
        Assert.Equal("Soto", current.PrimerApellido);
    }

    [Fact]
    public async Task DeleteAsync_CuandoNoExiste_RetornaFalse()
    {
        var repository = new FakeEstudianteRepository();
        var service = new EstudianteService(repository);

        var deleted = await service.DeleteAsync(Guid.NewGuid());

        Assert.False(deleted);
    }
}
