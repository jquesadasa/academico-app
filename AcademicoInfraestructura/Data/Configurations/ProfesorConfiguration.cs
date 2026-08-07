using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class ProfesorConfiguration : IEntityTypeConfiguration<Profesor>
{
    public void Configure(EntityTypeBuilder<Profesor> builder)
    {
        builder.ToTable("profesores");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Especialidad).HasColumnName("especialidad").IsRequired().HasMaxLength(200);
        builder.Property(p => p.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(p => p.CreatedAt).HasColumnName("created_at");
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
         builder.Ignore(p => p.Secciones);

         builder.HasMany(p => p.MonografiasSupervision)
             .WithOne(m => m.Supervisor)
             .HasForeignKey(m => m.SupervisorId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}
