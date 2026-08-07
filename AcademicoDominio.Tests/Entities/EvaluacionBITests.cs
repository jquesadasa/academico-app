using AcademicoDominio.Entities;

namespace AcademicoDominio.Tests.Entities;

public class EvaluacionBITests
{
    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    [InlineData(7)]
    public void BandaMinima_ValorValido_NoLanzaExcepcion(int banda)
    {
        var eval = new EvaluacionBI();
        var ex = Record.Exception(() => eval.BandaMinima = banda);
        Assert.Null(ex);
        Assert.Equal(banda, eval.BandaMinima);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(8)]
    [InlineData(-1)]
    public void BandaMinima_ValorInvalido_LanzaArgumentOutOfRange(int banda)
    {
        var eval = new EvaluacionBI();
        Assert.Throws<ArgumentOutOfRangeException>(() => eval.BandaMinima = banda);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    public void BandaAlcanzada_ValorValido_NoLanzaExcepcion(int banda)
    {
        var eval = new EvaluacionBI();
        var ex = Record.Exception(() => eval.BandaAlcanzada = banda);
        Assert.Null(ex);
        Assert.Equal(banda, eval.BandaAlcanzada);
    }

    [Fact]
    public void BandaAlcanzada_Null_EsPermitido()
    {
        var eval = new EvaluacionBI();
        eval.BandaAlcanzada = null;
        Assert.Null(eval.BandaAlcanzada);
    }

    [Fact]
    public void Aprobado_CuandoBandaAlcanzadaMayorIgualMinima_RetornaTrue()
    {
        var eval = new EvaluacionBI { BandaMinima = 4, BandaAlcanzada = 5 };
        Assert.True(eval.Aprobado);
    }

    [Fact]
    public void Aprobado_CuandoBandaAlcanzadaMenorMinima_RetornaFalse()
    {
        var eval = new EvaluacionBI { BandaMinima = 4, BandaAlcanzada = 3 };
        Assert.False(eval.Aprobado);
    }

    [Fact]
    public void Aprobado_SinBandaAlcanzada_RetornaFalse()
    {
        var eval = new EvaluacionBI { BandaMinima = 4 };
        Assert.False(eval.Aprobado);
    }

    [Fact]
    public void TotalAusentismo_SumaCorrectamente()
    {
        var eval = new EvaluacionBI
        {
            AusentismoTardias = 2,
            AusentismoInjustificadas = 3,
            AusentismoJustificadas = 1
        };
        Assert.Equal(6, eval.TotalAusentismo);
    }
}
