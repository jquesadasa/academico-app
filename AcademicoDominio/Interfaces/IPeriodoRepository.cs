using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IPeriodoRepository : IRepository<Periodo>
{
    Task<IReadOnlyList<Periodo>> GetActivosAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Periodo>> GetByAnioAsync(int anio, CancellationToken cancellationToken = default);
    Task<Periodo?> GetVigenteAsync(CancellationToken cancellationToken = default);
}
