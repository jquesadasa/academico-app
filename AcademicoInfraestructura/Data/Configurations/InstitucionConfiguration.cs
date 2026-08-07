using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class InstitucionConfiguration : IEntityTypeConfiguration<Institucion>
{
    public void Configure(EntityTypeBuilder<Institucion> builder)
    {
        builder.ToTable("instituciones");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnName("id");
        builder.Property(i => i.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(300);
        builder.Property(i => i.Codigo).HasColumnName("codigo").HasMaxLength(20);
        builder.Property(i => i.DireccionRegionalId).HasColumnName("direccion_regional_id");
        builder.Property(i => i.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(i => i.CreatedAt).HasColumnName("created_at");
        builder.Property(i => i.UpdatedAt).HasColumnName("updated_at");

         builder.HasOne(i => i.DireccionRegional)
             .WithMany(d => d.Instituciones)
             .HasForeignKey(i => i.DireccionRegionalId)
             .OnDelete(DeleteBehavior.Restrict);
         builder.Ignore(i => i.Secciones);
    }
}
