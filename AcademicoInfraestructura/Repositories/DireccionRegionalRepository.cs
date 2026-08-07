using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class DireccionRegionalRepository : BaseRepository<DireccionRegional>, IDireccionRegionalRepository
{
    public DireccionRegionalRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<DireccionRegional>> GetActivasAsync(CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().Where(d => d.Activo).OrderBy(d => d.Nombre).ToListAsync(cancellationToken);
}
