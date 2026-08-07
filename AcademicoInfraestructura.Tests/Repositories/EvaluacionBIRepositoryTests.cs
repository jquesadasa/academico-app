using AcademicoDominio.Entities;
using AcademicoInfraestructura.Repositories;
using AcademicoInfraestructura.Tests.Helpers;

namespace AcademicoInfraestructura.Tests.Repositories;

public class EvaluacionBIRepositoryTests
{
    private static async Task<(Estudiante e, Asignatura a, Seccion s, Periodo p)> SeedAsync(
        AcademicoInfraestructura.Data.ApplicationDbContext ctx)
    {
        var periodoRepo = new PeriodoRepository(ctx);
        var p = await periodoRepo.AddAsync(new Periodo
        {
            Nombre = "Sem 2024",
            FechaInicio = new DateTime(2024, 7, 1),
            FechaFin = new DateTime(2024, 12, 31),
            Anio = 2024
        });

        var seccionRepo = new SeccionRepository(ctx);
        var s = await seccionRepo.AddAsync(new Seccion { Codigo = "11-1", PeriodoId = p.Id });

        var estudianteRepo = new EstudianteRepository(ctx);
        var e = await estudianteRepo.AddAsync(new Estudiante { Cedula = "1111", NumeroLista = 1 });

        var asignaturaId = Guid.NewGuid();
        ctx.Asignaturas.Add(new Asignatura { Id = asignaturaId, Nombre = "Historia BI", TipoEvaluacion = "BI" });
        await ctx.SaveChangesAsync();
        var a = ctx.Asignaturas.First();

        return (e, a, s, p);
    }

    [Fact]
    public async Task AddAsync_EvaluacionBI_PersisteCorrecto()
    {
        using var ctx = TestDbContextFactory.Create();
        var (e, a, s, p) = await SeedAsync(ctx);
        var repo = new EvaluacionBIRepository(ctx);

        var eval = new EvaluacionBI
        {
            EstudianteId = e.Id,
            AsignaturaId = a.Id,
            SeccionId = s.Id,
            PeriodoId = p.Id,
            BandaMinima = 4,
            BandaAlcanzada = 6
        };
        var result = await repo.AddAsync(eval);

        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(6, result.BandaAlcanzada);
    }

    [Fact]
    public async Task GetByEstudianteAsync_RetornaEvaluacionesDelEstudiante()
    {
        using var ctx = TestDbContextFactory.Create();
        var (e, a, s, p) = await SeedAsync(ctx);
        var repo = new EvaluacionBIRepository(ctx);

        await repo.AddAsync(new EvaluacionBI
        {
            EstudianteId = e.Id, AsignaturaId = a.Id,
            SeccionId = s.Id, PeriodoId = p.Id, BandaMinima = 4
        });

        var lista = await repo.GetByEstudianteAsync(e.Id);
        Assert.Single(lista);
    }

    [Fact]
    public async Task GetBySeccionYPeriodoAsync_RetornaConsolidado()
    {
        using var ctx = TestDbContextFactory.Create();
        var (e, a, s, p) = await SeedAsync(ctx);
        var repo = new EvaluacionBIRepository(ctx);

        await repo.AddAsync(new EvaluacionBI
        {
            EstudianteId = e.Id, AsignaturaId = a.Id,
            SeccionId = s.Id, PeriodoId = p.Id, BandaMinima = 3, BandaAlcanzada = 5
        });

        var consolidado = await repo.GetBySeccionYPeriodoAsync(s.Id, p.Id);
        Assert.Single(consolidado);
        Assert.Equal(5, consolidado[0].BandaAlcanzada);
    }

    [Fact]
    public async Task GetByEstudianteAsignaturaSeccionAsync_RegistroExistente_RetornaEvaluacion()
    {
        using var ctx = TestDbContextFactory.Create();
        var (e, a, s, p) = await SeedAsync(ctx);
        var repo = new EvaluacionBIRepository(ctx);

        await repo.AddAsync(new EvaluacionBI
        {
            EstudianteId = e.Id, AsignaturaId = a.Id,
            SeccionId = s.Id, PeriodoId = p.Id, BandaMinima = 4
        });

        var found = await repo.GetByEstudianteAsignaturaSeccionAsync(e.Id, a.Id, s.Id);
        Assert.NotNull(found);
    }
}
