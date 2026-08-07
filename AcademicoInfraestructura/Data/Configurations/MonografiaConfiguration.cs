using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class MonografiaConfiguration : IEntityTypeConfiguration<Monografia>
{
    public void Configure(EntityTypeBuilder<Monografia> builder)
    {
        builder.ToTable("monografias");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("id");
        builder.Property(m => m.EstudianteId).HasColumnName("estudiante_id");
        builder.Property(m => m.SeccionId).HasColumnName("seccion_id");
        builder.Property(m => m.PeriodoId).HasColumnName("periodo_id");
        builder.Property(m => m.AreaInvestigacion).HasColumnName("area_investigacion").HasMaxLength(300);
        builder.Property(m => m.SupervisorNombre).HasColumnName("supervisor_nombre").HasMaxLength(300);
        builder.Property(m => m.SupervisorId).HasColumnName("supervisor_id");
        builder.Property(m => m.BandaAlcanzada).HasColumnName("banda_alcanzada");
        builder.Property(m => m.Observaciones).HasColumnName("observaciones");
        builder.Property(m => m.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(m => m.CreatedAt).HasColumnName("created_at");
        builder.Property(m => m.UpdatedAt).HasColumnName("updated_at");

        builder.HasIndex(m => new { m.EstudianteId, m.PeriodoId }).IsUnique();

         builder.HasOne(m => m.Estudiante)
             .WithMany(e => e.Monografias)
             .HasForeignKey(m => m.EstudianteId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(m => m.Seccion)
             .WithMany(s => s.Monografias)
             .HasForeignKey(m => m.SeccionId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(m => m.Periodo)
             .WithMany()
             .HasForeignKey(m => m.PeriodoId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(m => m.Supervisor)
             .WithMany(p => p.MonografiasSupervision)
             .HasForeignKey(m => m.SupervisorId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}
