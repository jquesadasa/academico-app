using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class ProgramaAcademicoRepository : BaseRepository<ProgramaAcademico>, IProgramaAcademicoRepository
{
    public ProgramaAcademicoRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<ProgramaAcademico>> GetActivosAsync(CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().Where(p => p.Activo).OrderBy(p => p.Nombre).ToListAsync(cancellationToken);
}
