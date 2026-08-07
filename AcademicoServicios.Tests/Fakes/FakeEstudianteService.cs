using AcademicoNegocio.Dtos;
using AcademicoNegocio.Interfaces;

namespace AcademicoServicios.Tests.Fakes;

internal sealed class FakeEstudianteService : IEstudianteService
{
    private readonly List<EstudianteDto> _items;

    public FakeEstudianteService(IEnumerable<EstudianteDto>? seed = null)
    {
        _items = seed?.ToList() ?? [];
    }

    public Task<IReadOnlyList<EstudianteDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<EstudianteDto>>(_items.ToList());

    public Task<EstudianteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_items.FirstOrDefault(x => x.Id == id));

    public Task<EstudianteDto> CreateAsync(EstudianteDto dto, CancellationToken cancellationToken = default)
    {
        var nextId = Guid.NewGuid();
        var created = dto with { Id = nextId };
        _items.Add(created);
        return Task.FromResult(created);
    }

    public Task<bool> UpdateAsync(Guid id, EstudianteDto dto, CancellationToken cancellationToken = default)
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

    public Task<EstudianteDto?> GetByCedulaAsync(string cedula, CancellationToken cancellationToken = default)
        => Task.FromResult(_items.FirstOrDefault(x => x.Cedula == cedula));

    public Task<IReadOnlyList<EstudianteDto>> GetActivosAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<EstudianteDto>>(_items.Where(x => x.Activo).ToList());
}

