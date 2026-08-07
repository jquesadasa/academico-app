using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface ITeoriaConocimientoRepository : IRepository<TeoriaConocimiento>
{
    Task<TeoriaConocimiento?> GetByEstudianteYPeriodoAsync(Guid estudianteId, Guid periodoId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TeoriaConocimiento>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TeoriaConocimiento>> GetConsolidadoGrupalAsync(Guid seccionId, Guid periodoId, CancellationToken cancellationToken = default);
}

