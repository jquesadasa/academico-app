using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface INivelRepository : IRepository<Nivel>
{
    Task<IReadOnlyList<Nivel>> GetActivosOrdenadosAsync(CancellationToken cancellationToken = default);
}
