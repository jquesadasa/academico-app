using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class ReporteConfiguration : IEntityTypeConfiguration<Reporte>
{
    public void Configure(EntityTypeBuilder<Reporte> builder)
    {
        builder.ToTable("reportes");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.Tipo).HasColumnName("tipo").HasMaxLength(100);
        builder.Property(r => r.EstudianteId).HasColumnName("estudiante_id");
        builder.Property(r => r.PeriodoId).HasColumnName("periodo_id");
        builder.Property(r => r.SeccionId).HasColumnName("seccion_id");
        builder.Property(r => r.FechaGeneracion).HasColumnName("fecha_generacion");

         builder.HasOne(r => r.Estudiante)
             .WithMany()
             .HasForeignKey(r => r.EstudianteId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(r => r.Periodo)
             .WithMany()
             .HasForeignKey(r => r.PeriodoId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(r => r.Seccion)
             .WithMany()
             .HasForeignKey(r => r.SeccionId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}
