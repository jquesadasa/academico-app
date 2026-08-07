using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class PermisoRepository : BaseRepository<Permiso>, IPermisoRepository
{
    public PermisoRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Permiso>> GetByModuloAsync(string modulo, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().Where(p => p.Modulo == modulo).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Permiso>> GetByRolAsync(Guid rolId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(p => p.Roles.Any(r => r.Id == rolId))
                      .ToListAsync(cancellationToken);
}
