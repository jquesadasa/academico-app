using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IProfesorRepository : IRepository<Profesor>
{
    Task<IReadOnlyList<Profesor>> GetActivosAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Profesor>> GetByEspecialidadAsync(string especialidad, CancellationToken cancellationToken = default);
}
