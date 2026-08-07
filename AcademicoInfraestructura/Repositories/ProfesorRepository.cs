using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class ProfesorRepository : BaseRepository<Profesor>, IProfesorRepository
{
    public ProfesorRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Profesor>> GetActivosAsync(CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(p => p.Activo)
                      .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Profesor>> GetByEspecialidadAsync(string especialidad, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(p => p.Especialidad.Contains(especialidad))
                      .ToListAsync(cancellationToken);
}
