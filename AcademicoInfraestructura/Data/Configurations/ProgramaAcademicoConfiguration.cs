using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class ProgramaAcademicoConfiguration : IEntityTypeConfiguration<ProgramaAcademico>
{
    public void Configure(EntityTypeBuilder<ProgramaAcademico> builder)
    {
        builder.ToTable("programas_academicos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(200);
        builder.Property(p => p.Descripcion).HasColumnName("descripcion");
        builder.Property(p => p.Codigo).HasColumnName("codigo").HasMaxLength(20);
        builder.Property(p => p.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(p => p.CreatedAt).HasColumnName("created_at");
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
        builder.Ignore(p => p.Secciones);
    }
}
