using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class SeccionConfiguration : IEntityTypeConfiguration<Seccion>
{
    public void Configure(EntityTypeBuilder<Seccion> builder)
    {
        builder.ToTable("secciones");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("id");
        builder.Property(s => s.Codigo).HasColumnName("codigo").IsRequired().HasMaxLength(20);
        builder.Property(s => s.PeriodoId).HasColumnName("periodo_id");
        builder.Property(s => s.CreatedAt).HasColumnName("created_at");
        builder.Ignore(s => s.Activo);
        builder.Ignore(s => s.Nombre);
        builder.Ignore(s => s.NivelId);
        builder.Ignore(s => s.ProgramaAcademicoId);
        builder.Ignore(s => s.InstitucionId);
        builder.Ignore(s => s.ProfesorGuiaId);
        builder.Ignore(s => s.Nivel);
        builder.Ignore(s => s.ProgramaAcademico);
        builder.Ignore(s => s.Institucion);
        builder.Ignore(s => s.ProfesorGuia);

        builder.HasIndex(s => s.Codigo).IsUnique();

         builder.HasOne(s => s.Periodo)
             .WithMany(p => p.Secciones)
             .HasForeignKey(s => s.PeriodoId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasMany(s => s.Matriculas)
             .WithOne(m => m.Seccion)
             .HasForeignKey(m => m.SeccionId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasMany(s => s.EvaluacionesBI)
             .WithOne(e => e.Seccion)
             .HasForeignKey(e => e.SeccionId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasMany(s => s.EvaluacionesNacionales)
             .WithOne(e => e.Seccion)
             .HasForeignKey(e => e.SeccionId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasMany(s => s.Monografias)
             .WithOne(m => m.Seccion)
             .HasForeignKey(m => m.SeccionId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasMany(s => s.TeoriasConocimiento)
             .WithOne(t => t.Seccion)
             .HasForeignKey(t => t.SeccionId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}
