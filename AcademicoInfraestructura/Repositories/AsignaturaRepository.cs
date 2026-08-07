using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class AsignaturaRepository : BaseRepository<Asignatura>, IAsignaturaRepository
{
    public AsignaturaRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Asignatura?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .FirstOrDefaultAsync(a => a.Nombre == nombre, cancellationToken);
}
