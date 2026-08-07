using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IRolRepository : IRepository<Rol>
{
    Task<Rol?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default);
    Task<Rol?> GetConPermisosAsync(Guid rolId, CancellationToken cancellationToken = default);
}
