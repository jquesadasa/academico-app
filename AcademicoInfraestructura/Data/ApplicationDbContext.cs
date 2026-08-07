using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Profesor> Profesores => Set<Profesor>();
    public DbSet<Asignatura> Asignaturas => Set<Asignatura>();
    public DbSet<Matricula> Matriculas => Set<Matricula>();
    public DbSet<Seccion> Secciones => Set<Seccion>();
    public DbSet<Periodo> Periodos => Set<Periodo>();
    public DbSet<Reporte> Reportes => Set<Reporte>();
    public DbSet<Auditoria> Auditorias => Set<Auditoria>();
    // Nuevas entidades BI
    public DbSet<DireccionRegional> DireccionesRegionales => Set<DireccionRegional>();
    public DbSet<Institucion> Instituciones => Set<Institucion>();
    public DbSet<ProgramaAcademico> ProgramasAcademicos => Set<ProgramaAcademico>();
    public DbSet<Nivel> Niveles => Set<Nivel>();
    public DbSet<EvaluacionBI> EvaluacionesBI => Set<EvaluacionBI>();
    public DbSet<EvaluacionNacional> EvaluacionesNacionales => Set<EvaluacionNacional>();
    public DbSet<Monografia> Monografias => Set<Monografia>();
    public DbSet<TeoriaConocimiento> TeoriasConocimiento => Set<TeoriaConocimiento>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<Permiso> Permisos => Set<Permiso>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
