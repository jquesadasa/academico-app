using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class MatriculaRepository : BaseRepository<Matricula>, IMatriculaRepository
{
    public MatriculaRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Matricula>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(m => m.EstudianteId == estudianteId)
                      .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Matricula>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(m => m.SeccionId == seccionId)
                      .ToListAsync(cancellationToken);

    public async Task<bool> ExisteMatriculaAsync(Guid estudianteId, Guid seccionId, CancellationToken cancellationToken = default)
        => await DbSet.AnyAsync(m => m.EstudianteId == estudianteId && m.SeccionId == seccionId, cancellationToken);
}

