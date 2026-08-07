using AcademicoDominio.Entities;

namespace AcademicoDominio.Tests.Entities;

public class EvaluacionNacionalTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(65)]
    [InlineData(100)]
    public void NotaMinima_ValorValido_NoLanzaExcepcion(decimal nota)
    {
        var eval = new EvaluacionNacional();
        var ex = Record.Exception(() => eval.NotaMinima = nota);
        Assert.Null(ex);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void NotaMinima_ValorInvalido_LanzaArgumentOutOfRange(decimal nota)
    {
        var eval = new EvaluacionNacional();
        Assert.Throws<ArgumentOutOfRangeException>(() => eval.NotaMinima = nota);
    }

    [Fact]
    public void Condicion_ConNotaObtenidaSuficiente_RetornaAprobado()
    {
        var eval = new EvaluacionNacional { NotaMinima = 65, NotaObtenida = 80 };
        Assert.Equal("Aprobado", eval.Condicion);
    }

    [Fact]
    public void Condicion_ConNotaObtenidaInsuficiente_RetornaReprobado()
    {
        var eval = new EvaluacionNacional { NotaMinima = 65, NotaObtenida = 60 };
        Assert.Equal("Reprobado", eval.Condicion);
    }

    [Fact]
    public void Condicion_SinNotaObtenida_RetornaPendiente()
    {
        var eval = new EvaluacionNacional { NotaMinima = 65 };
        Assert.Equal("Pendiente", eval.Condicion);
    }

    [Fact]
    public void Aprobado_ConNotaExactaMinima_RetornaTrue()
    {
        var eval = new EvaluacionNacional { NotaMinima = 65, NotaObtenida = 65 };
        Assert.True(eval.Aprobado);
    }
}
