using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class RolRepository : BaseRepository<Rol>, IRolRepository
{
    public RolRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Rol?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().FirstOrDefaultAsync(r => r.Nombre == nombre, cancellationToken);

    public async Task<Rol?> GetConPermisosAsync(Guid rolId, CancellationToken cancellationToken = default)
        => await DbSet.Include(r => r.Permisos).FirstOrDefaultAsync(r => r.Id == rolId, cancellationToken);
}
