using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface ISeccionRepository : IRepository<Seccion>
{
    Task<IReadOnlyList<Seccion>> GetByPeriodoAsync(Guid periodoId, CancellationToken cancellationToken = default);
    Task<Seccion?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default);
}

