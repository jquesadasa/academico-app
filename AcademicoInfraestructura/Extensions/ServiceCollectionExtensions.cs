using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using AcademicoInfraestructura.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace AcademicoInfraestructura.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment = false)
    {
        var connectionString = ResolveConnectionString(configuration);

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

    private static string ResolveConnectionString(IConfiguration configuration)
    {
        var directConnectionString = configuration.GetConnectionString("DefaultConnection");

        if (!string.IsNullOrWhiteSpace(directConnectionString) &&
            !directConnectionString.Contains("PLACEHOLDER_USE_USER_SECRETS", StringComparison.OrdinalIgnoreCase))
        {
            return directConnectionString;
        }

        var databaseUrl = configuration["DATABASE_URL"]
            ?? configuration["POSTGRES_URL"]
            ?? configuration["POSTGRESQL_URL"]
            ?? configuration["DATABASE_PUBLIC_URL"];

        if (!string.IsNullOrWhiteSpace(databaseUrl))
        {
            return ParseDatabaseUrl(databaseUrl);
        }

        throw new InvalidOperationException(
            "Database connection not configured. Set ConnectionStrings:DefaultConnection or DATABASE_URL/POSTGRES_URL environment variables.");
    }

    private static string ParseDatabaseUrl(string databaseUrl)
    {
        if (databaseUrl.Contains("Host=", StringComparison.OrdinalIgnoreCase))
        {
            return databaseUrl;
        }

        if (!Uri.TryCreate(databaseUrl, UriKind.Absolute, out var uri))
        {
            throw new InvalidOperationException("DATABASE_URL is not a valid URI.");
        }

        var userInfo = uri.UserInfo.Split(':', 2);
        if (userInfo.Length == 0 || string.IsNullOrWhiteSpace(userInfo[0]))
        {
            throw new InvalidOperationException("DATABASE_URL must include username credentials.");
        }

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port > 0 ? uri.Port : 5432,
            Database = uri.AbsolutePath.TrimStart('/'),
            Username = Uri.UnescapeDataString(userInfo[0]),
            Password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty,
            SslMode = SslMode.Require
        };

        return builder.ConnectionString;
    }
}
