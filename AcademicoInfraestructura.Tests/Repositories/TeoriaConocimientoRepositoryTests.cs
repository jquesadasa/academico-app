using AcademicoDominio.Entities;
using AcademicoInfraestructura.Repositories;
using AcademicoInfraestructura.Tests.Helpers;

namespace AcademicoInfraestructura.Tests.Repositories;

public class TeoriaConocimientoRepositoryTests
{
    [Fact]
    public async Task AddAsync_TdC_PersisteCorrecto()
    {
        using var ctx = TestDbContextFactory.Create();
        var p = await new PeriodoRepository(ctx).AddAsync(new Periodo
        {
            Nombre = "2024-II", FechaInicio = DateTime.UtcNow.AddDays(-1),
            FechaFin = DateTime.UtcNow.AddDays(30), Anio = 2024
        });
        var s = await new SeccionRepository(ctx).AddAsync(new Seccion { Codigo = "11-1", PeriodoId = p.Id });
        var e = await new EstudianteRepository(ctx).AddAsync(new Estudiante { Cedula = "T001" });

        var repo = new TeoriaConocimientoRepository(ctx);
        var tdc = await repo.AddAsync(new TeoriaConocimiento
        {
            EstudianteId = e.Id, SeccionId = s.Id, PeriodoId = p.Id,
            BandaAlcanzada = "B",
            AusentismoExhibicion = 0,
            ObservacionesExhibicion = "Buena presentación"
        });

        Assert.NotEqual(Guid.Empty, tdc.Id);
        Assert.Equal("B", tdc.BandaAlcanzada);
    }

    [Fact]
    public async Task GetConsolidadoGrupalAsync_RetornaRegistrosDeSeccionYPeriodo()
    {
        using var ctx = TestDbContextFactory.Create();
        var p = await new PeriodoRepository(ctx).AddAsync(new Periodo
        {
            Nombre = "2024-II", FechaInicio = DateTime.UtcNow.AddDays(-1),
            FechaFin = DateTime.UtcNow.AddDays(30), Anio = 2024
        });
        var s = await new SeccionRepository(ctx).AddAsync(new Seccion { Codigo = "11-2", PeriodoId = p.Id });
        var e1 = await new EstudianteRepository(ctx).AddAsync(new Estudiante { Cedula = "T101", NumeroLista = 1 });
        var e2 = await new EstudianteRepository(ctx).AddAsync(new Estudiante { Cedula = "T102", NumeroLista = 2 });

        var repo = new TeoriaConocimientoRepository(ctx);
        await repo.AddAsync(new TeoriaConocimiento { EstudianteId = e1.Id, SeccionId = s.Id, PeriodoId = p.Id, BandaAlcanzada = "A" });
        await repo.AddAsync(new TeoriaConocimiento { EstudianteId = e2.Id, SeccionId = s.Id, PeriodoId = p.Id, BandaAlcanzada = "C" });

        var lista = await repo.GetConsolidadoGrupalAsync(s.Id, p.Id);
        Assert.Equal(2, lista.Count);
    }
}
