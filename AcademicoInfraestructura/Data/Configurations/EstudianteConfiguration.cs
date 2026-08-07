using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class EstudianteConfiguration : IEntityTypeConfiguration<Estudiante>
{
    public void Configure(EntityTypeBuilder<Estudiante> builder)
    {
        builder.ToTable("estudiantes");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Cedula).HasColumnName("cedula").IsRequired().HasMaxLength(20);
        builder.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(150);
        builder.Property(e => e.PrimerApellido).HasColumnName("primer_apellido").HasMaxLength(150);
        builder.Property(e => e.SegundoApellido).HasColumnName("segundo apellido").HasMaxLength(150);
        builder.Ignore(e => e.NumeroLista);
        builder.Property(e => e.Iniciales).HasColumnName("iniciales").HasMaxLength(20);
        builder.Property(e => e.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(e => e.CreatedAt).HasColumnName("created_at");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");

        builder.HasIndex(e => e.Cedula).IsUnique();

        builder.HasMany(e => e.Matriculas)
               .WithOne(m => m.Estudiante)
               .HasForeignKey(m => m.EstudianteId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
