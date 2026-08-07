using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IEstudianteRepository : IRepository<Estudiante>
{
    Task<Estudiante?> GetByCedulaAsync(string cedula, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Estudiante>> GetActivosAsync(CancellationToken cancellationToken = default);
}
