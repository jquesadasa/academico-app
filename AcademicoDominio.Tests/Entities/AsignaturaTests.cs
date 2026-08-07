using AcademicoDominio.Entities;

namespace AcademicoDominio.Tests.Entities;

public class AsignaturaTests
{
    [Fact]
    public void EsBI_CuandoTipoEsBI_RetornaTrue()
    {
        var a = new Asignatura { TipoEvaluacion = "BI" };
        Assert.True(a.EsBI);
        Assert.False(a.EsNacional);
    }

    [Fact]
    public void EsNacional_CuandoTipoEsNacional_RetornaTrue()
    {
        var a = new Asignatura { TipoEvaluacion = "Nacional" };
        Assert.True(a.EsNacional);
        Assert.False(a.EsBI);
    }

    [Fact]
    public void Activo_PorDefecto_EsTrue()
    {
        var a = new Asignatura();
        Assert.True(a.Activo);
    }
}
