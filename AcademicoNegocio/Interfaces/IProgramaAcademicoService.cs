using AcademicoNegocio.Abstractions;
using AcademicoNegocio.Dtos;

namespace AcademicoNegocio.Interfaces;

public interface IProgramaAcademicoService : IApplicationService<ProgramaAcademicoDto>
{
    Task<IReadOnlyList<ProgramaAcademicoDto>> GetActivosAsync(CancellationToken cancellationToken = default);
}
