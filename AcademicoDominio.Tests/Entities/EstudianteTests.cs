using AcademicoDominio.Entities;

namespace AcademicoDominio.Tests.Entities;

public class EstudianteTests
{
    [Fact]
    public void NombreCompleto_ConTodosLosCampos_RetornaConcatenacion()
    {
        var e = new Estudiante
        {
            Nombre = "Juan",
            PrimerApellido = "Pérez",
            SegundoApellido = "Mora"
        };
        Assert.Equal("Juan Pérez Mora", e.NombreCompleto);
    }

    [Fact]
    public void NombreCompleto_SinSegundoApellido_RetornaNombreYPrimerApellido()
    {
        var e = new Estudiante { Nombre = "Ana", PrimerApellido = "López" };
        Assert.Equal("Ana López", e.NombreCompleto);
    }

    [Fact]
    public void Activo_PorDefecto_EsTrue()
    {
        var e = new Estudiante();
        Assert.True(e.Activo);
    }

    [Fact]
    public void CreatedAt_PorDefecto_EsFechaActual()
    {
        var antes = DateTime.UtcNow.AddSeconds(-1);
        var e = new Estudiante();
        Assert.True(e.CreatedAt >= antes);
    }
}
