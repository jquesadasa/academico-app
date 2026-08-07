using AcademicoDominio.Entities;

namespace AcademicoDominio.Tests.Entities;

public class TeoriaConocimientoTests
{
    [Theory]
    [InlineData("A")]
    [InlineData("B")]
    [InlineData("C")]
    [InlineData("D")]
    [InlineData("E")]
    public void BandaAlcanzada_ValorValido_NoLanzaExcepcion(string banda)
    {
        var tdc = new TeoriaConocimiento();
        var ex = Record.Exception(() => tdc.BandaAlcanzada = banda);
        Assert.Null(ex);
        Assert.Equal(banda, tdc.BandaAlcanzada);
    }

    [Theory]
    [InlineData("F")]
    [InlineData("G")]
    [InlineData("Z")]
    [InlineData("1")]
    public void BandaAlcanzada_ValorInvalido_LanzaArgumentException(string banda)
    {
        var tdc = new TeoriaConocimiento();
        Assert.Throws<ArgumentException>(() => tdc.BandaAlcanzada = banda);
    }

    [Fact]
    public void BandaAlcanzada_Null_EsPermitido()
    {
        var tdc = new TeoriaConocimiento();
        tdc.BandaAlcanzada = null;
        Assert.Null(tdc.BandaAlcanzada);
    }

    [Theory]
    [InlineData("a", "A")]
    [InlineData("b", "B")]
    [InlineData("e", "E")]
    public void BandaAlcanzada_EnMinuscula_SeConvierteAMayuscula(string input, string expected)
    {
        var tdc = new TeoriaConocimiento { BandaAlcanzada = input };
        Assert.Equal(expected, tdc.BandaAlcanzada);
    }
}
