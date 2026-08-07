using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IPermisoRepository : IRepository<Permiso>
{
    Task<IReadOnlyList<Permiso>> GetByModuloAsync(string modulo, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permiso>> GetByRolAsync(Guid rolId, CancellationToken cancellationToken = default);
}
