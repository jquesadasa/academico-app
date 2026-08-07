using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class ReporteRepository : BaseRepository<Reporte>, IReporteRepository
{
    public ReporteRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Reporte>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(r => r.EstudianteId == estudianteId)
                      .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Reporte>> GetByPeriodoAsync(Guid periodoId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(r => r.PeriodoId == periodoId)
                      .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Reporte>> GetByTipoAsync(string tipo, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(r => r.Tipo == tipo)
                      .ToListAsync(cancellationToken);
}

