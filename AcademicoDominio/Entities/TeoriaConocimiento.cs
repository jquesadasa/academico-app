namespace AcademicoDominio.Entities;

/// <summary>
/// Componente de TeorÃ­a del Conocimiento (TdC) del Bachillerato Internacional.
/// Banda evaluada en escala A-E.
/// </summary>
public class TeoriaConocimiento : BaseEntity
{
    private static readonly HashSet<string> BandasValidas = ["A", "B", "C", "D", "E"];

    public Guid EstudianteId { get; set; }
    public Guid SeccionId { get; set; }
    public Guid PeriodoId { get; set; }

    private string? _bandaAlcanzada;
    public string? BandaAlcanzada
    {
        get => _bandaAlcanzada;
        set
        {
            if (value is not null && !BandasValidas.Contains(value.ToUpper()))
                throw new ArgumentException("La banda debe ser A, B, C, D o E.", nameof(BandaAlcanzada));
            _bandaAlcanzada = value?.ToUpper();
        }
    }

    public int AusentismoExhibicion { get; set; }
    public int AusentismoOralidad { get; set; }
    public string? ObservacionesExhibicion { get; set; }
    public string? ObservacionesArgumentos { get; set; }
    public string? ObservacionesOralidad { get; set; }
    public string? ObservacionesEscritura { get; set; }

    // NavegaciÃ³n
    public Estudiante Estudiante { get; set; } = null!;
    public Seccion Seccion { get; set; } = null!;
    public Periodo Periodo { get; set; } = null!;
}

