using AcademicoDominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicoInfraestructura.Data.Configurations;

public class EvaluacionNacionalConfiguration : IEntityTypeConfiguration<EvaluacionNacional>
{
    public void Configure(EntityTypeBuilder<EvaluacionNacional> builder)
    {
        builder.ToTable("evaluaciones_nacionales");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
        builder.Property(e => e.AsignaturaId).HasColumnName("asignatura_id");
        builder.Property(e => e.SeccionId).HasColumnName("seccion_id");
        builder.Property(e => e.PeriodoId).HasColumnName("periodo_id");
        builder.Property(e => e.NotaMinima).HasColumnName("nota_minima").HasPrecision(5, 2);
        builder.Property(e => e.NotaObtenida).HasColumnName("nota_obtenida").HasPrecision(5, 2);
        builder.Property(e => e.NotaPruebaEstandarizada).HasColumnName("nota_prueba_estandarizada").HasPrecision(5, 2);
        builder.Property(e => e.AusentismoTardias).HasColumnName("ausentismo_tardias").HasDefaultValue(0);
        builder.Property(e => e.AusentismoInjustificadas).HasColumnName("ausentismo_injustificadas").HasDefaultValue(0);
        builder.Property(e => e.AusentismoJustificadas).HasColumnName("ausentismo_justificadas").HasDefaultValue(0);
        builder.Property(e => e.Observaciones).HasColumnName("observaciones");
        builder.Property(e => e.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(e => e.CreatedAt).HasColumnName("created_at");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");

        builder.Ignore(e => e.Condicion);
        builder.Ignore(e => e.Aprobado);
        builder.Ignore(e => e.TotalAusentismo);

        builder.HasIndex(e => new { e.EstudianteId, e.AsignaturaId, e.SeccionId }).IsUnique();

         builder.HasOne(e => e.Estudiante).WithMany(est => est.EvaluacionesNacionales)
             .HasForeignKey(e => e.EstudianteId).OnDelete(DeleteBehavior.Restrict);
         builder.HasOne(e => e.Asignatura).WithMany(a => a.EvaluacionesNacionales)
             .HasForeignKey(e => e.AsignaturaId).OnDelete(DeleteBehavior.Restrict);
         builder.HasOne(e => e.Seccion).WithMany(s => s.EvaluacionesNacionales)
             .HasForeignKey(e => e.SeccionId).OnDelete(DeleteBehavior.Restrict);
         builder.HasOne(e => e.Periodo).WithMany()
             .HasForeignKey(e => e.PeriodoId).OnDelete(DeleteBehavior.Restrict);

    }
}
