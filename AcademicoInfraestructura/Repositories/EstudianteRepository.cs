using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class EstudianteRepository : BaseRepository<Estudiante>, IEstudianteRepository
{
    public EstudianteRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Estudiante?> GetByCedulaAsync(string cedula, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .FirstOrDefaultAsync(e => e.Cedula == cedula, cancellationToken);

    public async Task<IReadOnlyList<Estudiante>> GetActivosAsync(CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(e => e.Activo)
                      .ToListAsync(cancellationToken);
}
