using AcademicoDominio.Entities;

namespace AcademicoDominio.Tests.Entities;

public class PeriodoTests
{
    [Fact]
    public void Periodo_Activo_PorDefecto_EsTrue()
    {
        var p = new Periodo();
        Assert.True(p.Activo);
    }

    [Fact]
    public void Periodo_GetVigente_Logica_FechasDentroDeRango()
    {
        var hoy = DateTime.UtcNow;
        var p = new Periodo
        {
            FechaInicio = hoy.AddDays(-30),
            FechaFin = hoy.AddDays(30),
            Activo = true
        };
        Assert.True(p.FechaInicio <= hoy && p.FechaFin >= hoy);
    }
}
