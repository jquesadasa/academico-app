namespace AcademicoNegocio.Abstractions;

public interface IApplicationService<TDto> where TDto : class, IIdentifiableDto
{
    Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, TDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

