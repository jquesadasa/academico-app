using AcademicoDominio.Entities;
using AcademicoInfraestructura.Repositories;
using AcademicoInfraestructura.Tests.Helpers;

namespace AcademicoInfraestructura.Tests.Repositories;

public class RolRepositoryTests
{
    [Fact]
    public async Task AddAsync_Rol_PersisteCorrecto()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new RolRepository(ctx);

        var rol = await repo.AddAsync(new Rol { Nombre = "Administrador", Descripcion = "Acceso total" });
        Assert.NotEqual(Guid.Empty, rol.Id);
        Assert.Equal("Administrador", rol.Nombre);
    }

    [Fact]
    public async Task GetByNombreAsync_RolExistente_RetornaRol()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new RolRepository(ctx);

        await repo.AddAsync(new Rol { Nombre = "Docente" });
        var found = await repo.GetByNombreAsync("Docente");

        Assert.NotNull(found);
        Assert.Equal("Docente", found.Nombre);
    }

    [Fact]
    public async Task GetConPermisosAsync_RetornaRolConPermisos()
    {
        using var ctx = TestDbContextFactory.Create();
        var repo = new RolRepository(ctx);
        var permisoRepo = new PermisoRepository(ctx);

        var permiso = await permisoRepo.AddAsync(new Permiso { Nombre = "Ver Reportes", Modulo = "Reportes" });
        var rol = new Rol { Nombre = "Coordinador" };
        rol.Permisos.Add(permiso);
        await repo.AddAsync(rol);

        var found = await repo.GetConPermisosAsync(rol.Id);
        Assert.NotNull(found);
        Assert.Single(found.Permisos);
        Assert.Equal("Ver Reportes", found.Permisos.First().Nombre);
    }
}
