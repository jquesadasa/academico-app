using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class AsignaturaConfiguration : IEntityTypeConfiguration<Asignatura>
{
    public void Configure(EntityTypeBuilder<Asignatura> builder)
    {
        builder.ToTable("asignaturas");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(200);
        builder.Property(a => a.Activo).HasColumnName("activa").HasDefaultValue(true);
        builder.Property(a => a.CreatedAt).HasColumnName("created_at");
        builder.Ignore(a => a.TipoEvaluacion);
        builder.Ignore(a => a.UpdatedAt);

        builder.HasMany(a => a.Secciones)
               .WithMany(s => s.Asignaturas)
               .UsingEntity<Dictionary<string, object>>(
                    "secciones_asignaturas",
                    right => right.HasOne<Seccion>()
                                  .WithMany()
                                  .HasForeignKey("seccion_id")
                                  .HasPrincipalKey(nameof(Seccion.Id))
                                  .OnDelete(DeleteBehavior.Restrict),
                    left => left.HasOne<Asignatura>()
                                .WithMany()
                                .HasForeignKey("asignatura_id")
                                .HasPrincipalKey(nameof(Asignatura.Id))
                                .OnDelete(DeleteBehavior.Restrict),
                    join =>
                    {
                        join.ToTable("secciones_asignaturas");
                        join.HasKey("seccion_id", "asignatura_id");
                    });

         builder.HasMany(a => a.EvaluacionesBI)
             .WithOne(e => e.Asignatura)
             .HasForeignKey(e => e.AsignaturaId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.HasMany(a => a.EvaluacionesNacionales)
             .WithOne(e => e.Asignatura)
             .HasForeignKey(e => e.AsignaturaId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}
