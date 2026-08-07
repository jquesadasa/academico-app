using System.Linq.Expressions;
using AcademicoDominio.Entities;
using AcademicoDominio.Interfaces;

namespace AcademicoNegocio.Tests.Fakes;

internal sealed class FakeEstudianteRepository : IEstudianteRepository
{
    private readonly List<Estudiante> _items = [];

    public Task<Estudiante?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_items.FirstOrDefault(x => x.Id == id));

    public Task<IReadOnlyList<Estudiante>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Estudiante>>(_items.ToList());

    public Task<IReadOnlyList<Estudiante>> FindAsync(Expression<Func<Estudiante, bool>> predicate, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Estudiante>>(_items.AsQueryable().Where(predicate).ToList());

    public Task<Estudiante> AddAsync(Estudiante entity, CancellationToken cancellationToken = default)
    {
        entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
        _items.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(Estudiante entity, CancellationToken cancellationToken = default)
    {
        var current = _items.First(x => x.Id == entity.Id);
        current.Cedula = entity.Cedula;
        current.Nombre = entity.Nombre;
        current.PrimerApellido = entity.PrimerApellido;
        current.SegundoApellido = entity.SegundoApellido;
        current.NumeroLista = entity.NumeroLista;
        current.Activo = entity.Activo;
        current.CreatedAt = entity.CreatedAt;
        current.UpdatedAt = entity.UpdatedAt;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Estudiante entity, CancellationToken cancellationToken = default)
    {
        _items.RemoveAll(x => x.Id == entity.Id);
        return Task.CompletedTask;
    }

    public Task<int> CountAsync(Expression<Func<Estudiante, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        var count = predicate is null ? _items.Count : _items.AsQueryable().Count(predicate);
        return Task.FromResult(count);
    }

    public Task<Estudiante?> GetByCedulaAsync(string cedula, CancellationToken cancellationToken = default)
        => Task.FromResult(_items.FirstOrDefault(x => x.Cedula == cedula));

    public Task<IReadOnlyList<Estudiante>> GetActivosAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Estudiante>>(_items.Where(x => x.Activo).ToList());
}

