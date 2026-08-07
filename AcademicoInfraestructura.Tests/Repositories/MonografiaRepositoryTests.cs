using AcademicoDominio.Entities;
using AcademicoInfraestructura.Repositories;
using AcademicoInfraestructura.Tests.Helpers;

namespace AcademicoInfraestructura.Tests.Repositories;

public class MonografiaRepositoryTests
{
    [Fact]
    public async Task AddAsync_Monografia_PersisteCorrecto()
    {
        using var ctx = TestDbContextFactory.Create();
        var periodoRepo = new PeriodoRepository(ctx);
        var p = await periodoRepo.AddAsync(new Periodo
        {
            Nombre = "2024-II", FechaInicio = DateTime.UtcNow.AddDays(-30),
            FechaFin = DateTime.UtcNow.AddDays(30), Anio = 2024
        });
        var secRepo = new SeccionRepository(ctx);
        var s = await secRepo.AddAsync(new Seccion { Codigo = "11-1", PeriodoId = p.Id });
        var estRepo = new EstudianteRepository(ctx);
        var e = await estRepo.AddAsync(new Estudiante { Cedula = "9999" });

        var repo = new MonografiaRepository(ctx);
        var mono = await repo.AddAsync(new Monografia
        {
            EstudianteId = e.Id,
            SeccionId = s.Id,
            PeriodoId = p.Id,
            AreaInvestigacion = "Ciencias Sociales",
            SupervisorNombre = "Prof. García",
            BandaAlcanzada = 5
        });

        Assert.NotEqual(Guid.Empty, mono.Id);
        Assert.Equal("Ciencias Sociales", mono.AreaInvestigacion);
    }

    [Fact]
    public async Task GetByEstudianteYPeriodoAsync_RegistroExistente_RetornaMonografia()
    {
        using var ctx = TestDbContextFactory.Create();
        var periodoRepo = new PeriodoRepository(ctx);
        var p = await periodoRepo.AddAsync(new Periodo
        {
            Nombre = "2024-II", FechaInicio = DateTime.UtcNow.AddDays(-10),
            FechaFin = DateTime.UtcNow.AddDays(10), Anio = 2024
        });
        var secRepo = new SeccionRepository(ctx);
        var s = await secRepo.AddAsync(new Seccion { Codigo = "11-2", PeriodoId = p.Id });
        var estRepo = new EstudianteRepository(ctx);
        var e = await estRepo.AddAsync(new Estudiante { Cedula = "8888" });

        var repo = new MonografiaRepository(ctx);
        await repo.AddAsync(new Monografia
        {
            EstudianteId = e.Id, SeccionId = s.Id, PeriodoId = p.Id,
            AreaInvestigacion = "Matemática"
        });

        var found = await repo.GetByEstudianteYPeriodoAsync(e.Id, p.Id);
        Assert.NotNull(found);
        Assert.Equal("Matemática", found.AreaInvestigacion);
    }

    [Fact]
    public async Task GetBySeccionAsync_RetornaTodasMonografiasDeSeccion()
    {
        using var ctx = TestDbContextFactory.Create();
        var periodoRepo = new PeriodoRepository(ctx);
        var p = await periodoRepo.AddAsync(new Periodo
        {
            Nombre = "2024-II", FechaInicio = DateTime.UtcNow.AddDays(-5),
            FechaFin = DateTime.UtcNow.AddDays(5), Anio = 2024
        });
        var secRepo = new SeccionRepository(ctx);
        var s = await secRepo.AddAsync(new Seccion { Codigo = "11-3", PeriodoId = p.Id });
        var estRepo = new EstudianteRepository(ctx);
        var e1 = await estRepo.AddAsync(new Estudiante { Cedula = "7001", NumeroLista = 1 });
        var e2 = await estRepo.AddAsync(new Estudiante { Cedula = "7002", NumeroLista = 2 });

        var repo = new MonografiaRepository(ctx);
        await repo.AddAsync(new Monografia { EstudianteId = e1.Id, SeccionId = s.Id, PeriodoId = p.Id });
        await repo.AddAsync(new Monografia { EstudianteId = e2.Id, SeccionId = s.Id, PeriodoId = p.Id });

        var lista = await repo.GetBySeccionAsync(s.Id);
        Assert.Equal(2, lista.Count);
    }
}
