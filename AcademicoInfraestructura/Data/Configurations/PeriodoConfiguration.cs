using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class PeriodoConfiguration : IEntityTypeConfiguration<Periodo>
{
    public void Configure(EntityTypeBuilder<Periodo> builder)
    {
        builder.ToTable("periodos");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(100);
        builder.Property(p => p.FechaInicio).HasColumnName("fecha_inicio");
        builder.Property(p => p.FechaFin).HasColumnName("fecha_fin");
        builder.Property(p => p.Anio).HasColumnName("anio");
        builder.Property(p => p.Estado).HasColumnName("estado").HasMaxLength(50);
        builder.Property(p => p.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(p => p.CreatedAt).HasColumnName("created_at");
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");

        builder.HasMany(p => p.Secciones)
               .WithOne(s => s.Periodo)
               .HasForeignKey(s => s.PeriodoId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
