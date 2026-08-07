using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class MonografiaRepository : BaseRepository<Monografia>, IMonografiaRepository
{
    public MonografiaRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Monografia?> GetByEstudianteYPeriodoAsync(Guid estudianteId, Guid periodoId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .FirstOrDefaultAsync(m => m.EstudianteId == estudianteId && m.PeriodoId == periodoId, cancellationToken);

    public async Task<IReadOnlyList<Monografia>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(m => m.SeccionId == seccionId)
                      .OrderBy(m => m.EstudianteId)
                      .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Monografia>> GetBySupervisorAsync(Guid supervisorId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(m => m.SupervisorId == supervisorId).ToListAsync(cancellationToken);
}

