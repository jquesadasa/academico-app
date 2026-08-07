using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;
using AcademicoInfraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicoInfraestructura.Repositories;

public class AuditoriaRepository : BaseRepository<Auditoria>, IAuditoriaRepository
{
    public AuditoriaRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Auditoria>> GetByUsuarioAsync(string usuarioId, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(a => a.UsuarioId == usuarioId)
                      .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Auditoria>> GetByAccionAsync(string accion, CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking()
                      .Where(a => a.Accion == accion)
                      .ToListAsync(cancellationToken);
}
