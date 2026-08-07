using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IAuditoriaRepository : IRepository<Auditoria>
{
    Task<IReadOnlyList<Auditoria>> GetByUsuarioAsync(string usuarioId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Auditoria>> GetByAccionAsync(string accion, CancellationToken cancellationToken = default);
}
