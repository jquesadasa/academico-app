using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class EvaluacionNacionalRepository : BaseRepository<EvaluacionNacional>, IEvaluacionNacionalRepository
{
    public EvaluacionNacionalRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<EvaluacionNacional>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(e => e.EstudianteId == estudianteId).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<EvaluacionNacional>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(e => e.SeccionId == seccionId).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<EvaluacionNacional>> GetBySeccionYPeriodoAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(e => e.SeccionId == seccionId && e.PeriodoId == periodoId)
                      .OrderBy(e => e.EstudianteId).ThenBy(e => e.AsignaturaId)
                      .ToListAsync(cancellationToken);

    public async Task<EvaluacionNacional?> GetByEstudianteAsignaturaSeccionAsync(Guid estudianteId, Guid asignaturaId, Guid seccionId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId && e.AsignaturaId == asignaturaId && e.SeccionId == seccionId, cancellationToken);

    public async Task<IReadOnlyList<EvaluacionNacional>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(e => e.SeccionId == seccionId && e.PeriodoId == periodoId)
                      .OrderBy(e => e.EstudianteId)
                      .ThenBy(e => e.AsignaturaId)
                      .ToListAsync(cancellationToken);
}

