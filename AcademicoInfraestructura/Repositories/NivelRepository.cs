using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class NivelRepository : BaseRepository<Nivel>, INivelRepository
{
    public NivelRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Nivel>> GetActivosOrdenadosAsync(CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().Where(n => n.Activo).OrderBy(n => n.Orden).ToListAsync(cancellationToken);
}
