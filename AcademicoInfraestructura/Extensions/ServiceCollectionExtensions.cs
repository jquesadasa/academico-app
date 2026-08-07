using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using AcademicoInfraestructura.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicoInfraestructura.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment = false)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        var useInMemorySetting = configuration["Data:UseInMemoryInDevelopment"];
        var useInMemoryInDevelopment = bool.TryParse(useInMemorySetting, out var parsedValue) && parsedValue;
        var useInMemory = isDevelopment && useInMemoryInDevelopment;

        if (useInMemory)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("AcademicoDevDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name)
                )
            );
        }

        // Repositorios
        services.AddScoped<IEstudianteRepository, EstudianteRepository>();
        services.AddScoped<IProfesorRepository, ProfesorRepository>();
        services.AddScoped<IAsignaturaRepository, AsignaturaRepository>();
        services.AddScoped<IMatriculaRepository, MatriculaRepository>();
        services.AddScoped<ISeccionRepository, SeccionRepository>();
        services.AddScoped<IPeriodoRepository, PeriodoRepository>();
        services.AddScoped<IReporteRepository, ReporteRepository>();
        services.AddScoped<IAuditoriaRepository, AuditoriaRepository>();
        // Nuevos repositorios BI
        services.AddScoped<IDireccionRegionalRepository, DireccionRegionalRepository>();
        services.AddScoped<IInstitucionRepository, InstitucionRepository>();
        services.AddScoped<IProgramaAcademicoRepository, ProgramaAcademicoRepository>();
        services.AddScoped<INivelRepository, NivelRepository>();
        services.AddScoped<IEvaluacionBIRepository, EvaluacionBIRepository>();
        services.AddScoped<IEvaluacionNacionalRepository, EvaluacionNacionalRepository>();
        services.AddScoped<IMonografiaRepository, MonografiaRepository>();
        services.AddScoped<ITeoriaConocimientoRepository, TeoriaConocimientoRepository>();
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<IPermisoRepository, PermisoRepository>();

        return services;
    }
}
