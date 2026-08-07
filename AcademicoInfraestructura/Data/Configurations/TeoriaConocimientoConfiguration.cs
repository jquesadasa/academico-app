using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class TeoriaConocimientoConfiguration : IEntityTypeConfiguration<TeoriaConocimiento>
{
    public void Configure(EntityTypeBuilder<TeoriaConocimiento> builder)
    {
        builder.ToTable("teorias_conocimiento");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.EstudianteId).HasColumnName("estudiante_id");
        builder.Property(t => t.SeccionId).HasColumnName("seccion_id");
        builder.Property(t => t.PeriodoId).HasColumnName("periodo_id");
        builder.Property(t => t.BandaAlcanzada).HasColumnName("banda_alcanzada").HasMaxLength(1);
        builder.Property(t => t.AusentismoExhibicion).HasColumnName("ausentismo_exhibicion").HasDefaultValue(0);
        builder.Property(t => t.AusentismoOralidad).HasColumnName("ausentismo_oralidad").HasDefaultValue(0);
        builder.Property(t => t.ObservacionesExhibicion).HasColumnName("observaciones_exhibicion");
        builder.Property(t => t.ObservacionesArgumentos).HasColumnName("observaciones_argumentos");
        builder.Property(t => t.ObservacionesOralidad).HasColumnName("observaciones_oralidad");
        builder.Property(t => t.ObservacionesEscritura).HasColumnName("observaciones_escritura");
        builder.Property(t => t.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(t => t.CreatedAt).HasColumnName("created_at");
        builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");

        builder.HasIndex(t => new { t.EstudianteId, t.PeriodoId }).IsUnique();

         builder.HasOne(t => t.Estudiante)
             .WithMany(e => e.TeoriasConocimiento)
             .HasForeignKey(t => t.EstudianteId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(t => t.Seccion)
             .WithMany(s => s.TeoriasConocimiento)
             .HasForeignKey(t => t.SeccionId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(t => t.Periodo)
             .WithMany()
             .HasForeignKey(t => t.PeriodoId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}
