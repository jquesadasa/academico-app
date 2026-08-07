using AcademicoDominio.Entities;
using AcademicoInfraestructura.Repositories;
using AcademicoInfraestructura.Tests.Helpers;

namespace AcademicoInfraestructura.Tests.Repositories;

public class PeriodoRepositoryTests
{
    [Fact]
    public async Task GetActivosAsync_SoloRetornaActivos()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new PeriodoRepository(ctx);

        await repo.AddAsync(new Periodo { Nombre = "2024-I", FechaInicio = DateTime.UtcNow, FechaFin = DateTime.UtcNow.AddDays(180), Anio = 2024, Activo = true });
        await repo.AddAsync(new Periodo { Nombre = "2023-II", FechaInicio = DateTime.UtcNow.AddDays(-365), FechaFin = DateTime.UtcNow.AddDays(-180), Anio = 2023, Activo = false });

        var activos = await repo.GetActivosAsync();
        Assert.Single(activos);
        Assert.Equal("2024-I", activos[0].Nombre);
    }

    [Fact]
    public async Task GetByAnioAsync_RetornaPeriodosDelAnio()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new PeriodoRepository(ctx);

        await repo.AddAsync(new Periodo { Nombre = "2024-I", FechaInicio = DateTime.UtcNow, FechaFin = DateTime.UtcNow.AddDays(90), Anio = 2024 });
        await repo.AddAsync(new Periodo { Nombre = "2024-II", FechaInicio = DateTime.UtcNow.AddDays(90), FechaFin = DateTime.UtcNow.AddDays(180), Anio = 2024 });
        await repo.AddAsync(new Periodo { Nombre = "2023-II", FechaInicio = DateTime.UtcNow.AddDays(-180), FechaFin = DateTime.UtcNow.AddDays(-90), Anio = 2023 });

        var periodos2024 = await repo.GetByAnioAsync(2024);
        Assert.Equal(2, periodos2024.Count);
    }

    [Fact]
    public async Task GetVigenteAsync_CuandoHayPeriodoActivo_RetornaPeriodo()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new PeriodoRepository(ctx);

        var hoy = DateTime.UtcNow;
        await repo.AddAsync(new Periodo
        {
            Nombre = "Vigente",
            FechaInicio = hoy.AddDays(-30),
            FechaFin = hoy.AddDays(30),
            Anio = hoy.Year,
            Activo = true
        });

        var vigente = await repo.GetVigenteAsync();
        Assert.NotNull(vigente);
        Assert.Equal("Vigente", vigente.Nombre);
    }
}
