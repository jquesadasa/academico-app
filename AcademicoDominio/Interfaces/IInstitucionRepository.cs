using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IInstitucionRepository : IRepository<Institucion>
{
    Task<IReadOnlyList<Institucion>> GetActivasAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Institucion>> GetByDireccionRegionalAsync(Guid direccionRegionalId, CancellationToken cancellationToken = default);
}

