using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class TeoriaConocimientoRepository : BaseRepository<TeoriaConocimiento>, ITeoriaConocimientoRepository
{
    public TeoriaConocimientoRepository(ApplicationDbContext context) : base(context) { }

    public async Task<TeoriaConocimiento?> GetByEstudianteYPeriodoAsync(Guid estudianteId, Guid periodoId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .FirstOrDefaultAsync(t => t.EstudianteId == estudianteId && t.PeriodoId == periodoId, cancellationToken);

    public async Task<IReadOnlyList<TeoriaConocimiento>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(t => t.SeccionId == seccionId)
                      .OrderBy(t => t.EstudianteId)
                      .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<TeoriaConocimiento>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(t => t.SeccionId == seccionId && t.PeriodoId == periodoId)
                      .OrderBy(t => t.EstudianteId)
                      .ToListAsync(cancellationToken);
}

