using AcademicoDominio.Entities;

namespace AcademicoDominio.Interfaces;

public interface IProgramaAcademicoRepository : IRepository<ProgramaAcademico>
{
    Task<IReadOnlyList<ProgramaAcademico>> GetActivosAsync(CancellationToken cancellationToken = default);
}
