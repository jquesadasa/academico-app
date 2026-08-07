using AcademicoDominio.Entities;
using AcademicoInfraestructura.Repositories;
using AcademicoInfraestructura.Tests.Helpers;

namespace AcademicoInfraestructura.Tests.Repositories;

public class EstudianteRepositoryTests
{
    [Fact]
    public async Task AddAsync_EstudianteValido_PersisteCorrecto()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new EstudianteRepository(ctx);

        var estudiante = new Estudiante { Cedula = "1-2345-6789", Nombre = "María", PrimerApellido = "Solano" };
        var result = await repo.AddAsync(estudiante);

        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("1-2345-6789", result.Cedula);
    }

    [Fact]
    public async Task GetByIdAsync_IdExistente_RetornaEstudiante()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new EstudianteRepository(ctx);

        var e = await repo.AddAsync(new Estudiante { Cedula = "111", Nombre = "Carlos" });
        var found = await repo.GetByIdAsync(e.Id);

        Assert.NotNull(found);
        Assert.Equal("111", found.Cedula);
    }

    [Fact]
    public async Task GetByIdAsync_IdInexistente_RetornaNull()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new EstudianteRepository(ctx);

        var result = await repo.GetByIdAsync(Guid.NewGuid());
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByCedulaAsync_CedulaExistente_RetornaEstudiante()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new EstudianteRepository(ctx);

        await repo.AddAsync(new Estudiante { Cedula = "ABC-123" });
        var found = await repo.GetByCedulaAsync("ABC-123");

        Assert.NotNull(found);
    }

    [Fact]
    public async Task GetActivosAsync_SoloRetornaActivos()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new EstudianteRepository(ctx);

        await repo.AddAsync(new Estudiante { Cedula = "001", Activo = true });
        await repo.AddAsync(new Estudiante { Cedula = "002", Activo = false });

        var activos = await repo.GetActivosAsync();
        Assert.Single(activos);
        Assert.Equal("001", activos[0].Cedula);
    }

    [Fact]
    public async Task UpdateAsync_ModificaPropiedad()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new EstudianteRepository(ctx);

        var e = await repo.AddAsync(new Estudiante { Cedula = "333", Nombre = "Pedro" });
        e.Nombre = "Pedro Actualizado";
        await repo.UpdateAsync(e);

        var updated = await repo.GetByIdAsync(e.Id);
        Assert.Equal("Pedro Actualizado", updated!.Nombre);
    }

    [Fact]
    public async Task DeleteAsync_EliminaEstudiante()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new EstudianteRepository(ctx);

        var e = await repo.AddAsync(new Estudiante { Cedula = "444" });
        await repo.DeleteAsync(e);

        var deleted = await repo.GetByIdAsync(e.Id);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task CountAsync_RetornaCantidadCorrecta()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new EstudianteRepository(ctx);

        await repo.AddAsync(new Estudiante { Cedula = "E1" });
        await repo.AddAsync(new Estudiante { Cedula = "E2" });
        await repo.AddAsync(new Estudiante { Cedula = "E3" });

        var count = await repo.CountAsync();
        Assert.Equal(3, count);
    }
}
