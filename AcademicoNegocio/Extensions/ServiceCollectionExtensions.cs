using AcademicoNegocio.Interfaces;
using AcademicoNegocio.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicoNegocio.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IEstudianteService, EstudianteService>();
        services.AddScoped<IProfesorService, ProfesorService>();
        services.AddScoped<IAsignaturaService, AsignaturaService>();
        services.AddScoped<ISeccionService, SeccionService>();
        services.AddScoped<IPeriodoService, PeriodoService>();
        services.AddScoped<IDireccionRegionalService, DireccionRegionalService>();
        services.AddScoped<IInstitucionService, InstitucionService>();
        services.AddScoped<IProgramaAcademicoService, ProgramaAcademicoService>();
        services.AddScoped<INivelService, NivelService>();
        services.AddScoped<IMonografiaService, MonografiaService>();
        services.AddScoped<ITeoriaConocimientoService, TeoriaConocimientoService>();
        services.AddScoped<IEvaluacionBIService, EvaluacionBIService>();
        services.AddScoped<IEvaluacionNacionalService, EvaluacionNacionalService>();
        services.AddScoped<IReporteAcademicoService, ReporteAcademicoService>();

        return services;
    }
}
