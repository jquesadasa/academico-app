using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class InstitucionRepository : BaseRepository<Institucion>, IInstitucionRepository
{
    public InstitucionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Institucion>> GetActivasAsync(CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().Where(i => i.Activo).OrderBy(i => i.Nombre).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Institucion>> GetByDireccionRegionalAsync(Guid direccionRegionalId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().Where(i => i.DireccionRegionalId == direccionRegionalId).ToListAsync(cancellationToken);
}

