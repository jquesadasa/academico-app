using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("matriculas");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("id");
        builder.Property(m => m.EstudianteId).HasColumnName("estudiante_id");
        builder.Property(m => m.SeccionId).HasColumnName("seccion_id");
        builder.Property(m => m.Estado).HasColumnName("estado").HasMaxLength(50);
        builder.Property(m => m.CreatedAt).HasColumnName("created_at");

        builder.HasIndex(m => new { m.EstudianteId, m.SeccionId }).IsUnique();

         builder.HasOne(m => m.Estudiante)
             .WithMany(e => e.Matriculas)
             .HasForeignKey(m => m.EstudianteId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(m => m.Seccion)
             .WithMany(s => s.Matriculas)
             .HasForeignKey(m => m.SeccionId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}
