using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class PeriodoRepository : BaseRepository<Periodo>, IPeriodoRepository
{
    public PeriodoRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Periodo>> GetActivosAsync(CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(p => p.Activo)
                      .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Periodo>> GetByAnioAsync(int anio, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(p => p.Anio == anio)
                      .ToListAsync(cancellationToken);

    public async Task<Periodo?> GetVigenteAsync(CancellationToken cancellationToken = default)
    {
        var hoy = DateTime.UtcNow;
        return await DbSet.AsNoTracking()
                          .Where(p => p.Activo && p.FechaInicio <= hoy && p.FechaFin >= hoy)
                          .OrderByDescending(p => p.FechaInicio)
                          .FirstOrDefaultAsync(cancellationToken);
    }
}
