using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class NivelConfiguration : IEntityTypeConfiguration<Nivel>
{
    public void Configure(EntityTypeBuilder<Nivel> builder)
    {
        builder.ToTable("niveles");
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasColumnName("id");
        builder.Property(n => n.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(100);
        builder.Property(n => n.Orden).HasColumnName("orden");
        builder.Property(n => n.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(n => n.CreatedAt).HasColumnName("created_at");
        builder.Property(n => n.UpdatedAt).HasColumnName("updated_at");
        builder.Ignore(n => n.Secciones);
    }
}
