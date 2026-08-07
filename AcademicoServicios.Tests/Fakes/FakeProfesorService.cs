using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoServicios.Tests.Fakes;

internal sealed class FakeProfesorService : IProfesorService
{
    private readonly List<ProfesorDto> _items;

    public FakeProfesorService(IEnumerable<ProfesorDto>? seed = null)
    {
        _items = seed?.ToList() ?? [];
    }

    public Task<IReadOnlyList<ProfesorDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<ProfesorDto>>(_items.ToList());

    public Task<ProfesorDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_items.FirstOrDefault(x => x.Id == id));

    public Task<ProfesorDto> CreateAsync(ProfesorDto dto, CancellationToken cancellationToken = default)
    {
        var nextId = Guid.NewGuid();
        var created = dto with { Id = nextId };
        _items.Add(created);
        return Task.FromResult(created);
    }

    public Task<bool> UpdateAsync(Guid id, ProfesorDto dto, CancellationToken cancellationToken = default)
    {
        var index = _items.FindIndex(x => x.Id == id);
        if (index < 0)
        {
            return Task.FromResult(false);
        }

        _items[index] = dto;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var removed = _items.RemoveAll(x => x.Id == id);
        return Task.FromResult(removed > 0);
    }

    public Task<IReadOnlyList<ProfesorDto>> GetActivosAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<ProfesorDto>>(_items.Where(x => x.Activo).ToList());

    public Task<IReadOnlyList<ProfesorDto>> GetByEspecialidadAsync(string especialidad, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<ProfesorDto>>(_items.Where(x => x.Especialidad == especialidad).ToList());
}
