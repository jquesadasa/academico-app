using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(100);
        builder.Property(r => r.Descripcion).HasColumnName("descripcion");
        builder.Property(r => r.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(r => r.CreatedAt).HasColumnName("created_at");
        builder.Property(r => r.UpdatedAt).HasColumnName("updated_at");

        builder.HasIndex(r => r.Nombre).IsUnique();

        builder.HasMany(r => r.Permisos)
               .WithMany(p => p.Roles)
               .UsingEntity(j => j.ToTable("roles_permisos"));
    }
}
