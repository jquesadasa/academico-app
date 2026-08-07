using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class DireccionRegionalConfiguration : IEntityTypeConfiguration<DireccionRegional>
{
    public void Configure(EntityTypeBuilder<DireccionRegional> builder)
    {
        builder.ToTable("direcciones_regionales");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id");
        builder.Property(d => d.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(200);
        builder.Property(d => d.Codigo).HasColumnName("codigo").HasMaxLength(20);
        builder.Property(d => d.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(d => d.CreatedAt).HasColumnName("created_at");
        builder.Property(d => d.UpdatedAt).HasColumnName("updated_at");

        builder.HasMany(d => d.Instituciones)
               .WithOne(i => i.DireccionRegional)
               .HasForeignKey(i => i.DireccionRegionalId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
