using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IDireccionRegionalRepository : IRepository<DireccionRegional>
{
    Task<IReadOnlyList<DireccionRegional>> GetActivasAsync(CancellationToken cancellationToken = default);
}
