using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IAsignaturaRepository : IRepository<Asignatura>
{
    Task<Asignatura?> GetByNombreAsync(string nombre, CancellationToken cancellationToken = default);
}
