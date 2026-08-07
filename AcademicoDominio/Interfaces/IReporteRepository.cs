using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IReporteRepository : IRepository<Reporte>
{
    Task<IReadOnlyList<Reporte>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Reporte>> GetByPeriodoAsync(Guid periodoId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Reporte>> GetByTipoAsync(string tipo, CancellationToken cancellationToken = default);
}

