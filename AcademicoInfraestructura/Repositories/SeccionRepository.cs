using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class SeccionRepository : BaseRepository<Seccion>, ISeccionRepository
{
    public SeccionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Seccion>> GetByPeriodoAsync(Guid periodoId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(s => s.PeriodoId == periodoId)
                      .ToListAsync(cancellationToken);

    public async Task<Seccion?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .FirstOrDefaultAsync(s => s.Codigo == codigo, cancellationToken);
}

