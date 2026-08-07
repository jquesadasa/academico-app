namespace AcademicoDominio.Entities;

/// <summary>
/// EvaluaciÃ³n de Bachillerato Internacional por bandas (1-7).
/// </summary>
public class EvaluacionBI : BaseEntity
{
    public Guid EstudianteId { get; set; }
    public Guid AsignaturaId { get; set; }
    public Guid SeccionId { get; set; }
    public Guid PeriodoId { get; set; }

    private int _bandaMinima;
    public int BandaMinima
    {
        get => _bandaMinima;
        set
        {
            if (value < 1 || value > 7)
                throw new ArgumentOutOfRangeException(nameof(BandaMinima), "La banda mÃ­nima debe estar entre 1 y 7.");
            _bandaMinima = value;
        }
    }

    private int? _bandaAlcanzada;
    public int? BandaAlcanzada
    {
        get => _bandaAlcanzada;
        set
        {
            if (value.HasValue && (value < 1 || value > 7))
                throw new ArgumentOutOfRangeException(nameof(BandaAlcanzada), "La banda alcanzada debe estar entre 1 y 7.");
            _bandaAlcanzada = value;
        }
    }

    public int AusentismoTardias { get; set; }
    public int AusentismoInjustificadas { get; set; }
    public int AusentismoJustificadas { get; set; }
    public string? Observaciones { get; set; }

    public bool Aprobado => BandaAlcanzada.HasValue && BandaAlcanzada >= BandaMinima;
    public int TotalAusentismo => AusentismoTardias + AusentismoInjustificadas + AusentismoJustificadas;

    // NavegaciÃ³n
    public Estudiante Estudiante { get; set; } = null!;
    public Asignatura Asignatura { get; set; } = null!;
    public Seccion Seccion { get; set; } = null!;
    public Periodo Periodo { get; set; } = null!;
}

