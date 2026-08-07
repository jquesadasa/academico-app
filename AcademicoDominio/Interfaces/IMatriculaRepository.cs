using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IMatriculaRepository : IRepository<Matricula>
{
    Task<IReadOnlyList<Matricula>> GetByEstudianteAsync(Guid estudianteId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Matricula>> GetBySeccionAsync(Guid seccionId, CancellationToken cancellationToken = default);
    Task<bool> ExisteMatriculaAsync(Guid estudianteId, Guid seccionId, CancellationToken cancellationToken = default);
}

