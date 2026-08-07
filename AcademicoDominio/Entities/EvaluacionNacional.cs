namespace AcademicoDominio.Entities;

/// <summary>
/// EvaluaciÃ³n de asignaturas nacionales por nota porcentual (0-100).
/// </summary>
public class EvaluacionNacional : BaseEntity
{
    public Guid EstudianteId { get; set; }
    public Guid AsignaturaId { get; set; }
    public Guid SeccionId { get; set; }
    public Guid PeriodoId { get; set; }

    private decimal _notaMinima;
    public decimal NotaMinima
    {
        get => _notaMinima;
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException(nameof(NotaMinima), "La nota mÃ­nima debe estar entre 0 y 100.");
            _notaMinima = value;
        }
    }

    private decimal? _notaObtenida;
    public decimal? NotaObtenida
    {
        get => _notaObtenida;
        set
        {
            if (value.HasValue && (value < 0 || value > 100))
                throw new ArgumentOutOfRangeException(nameof(NotaObtenida), "La nota obtenida debe estar entre 0 y 100.");
            _notaObtenida = value;
        }
    }

    public decimal? NotaPruebaEstandarizada { get; set; }
    public int AusentismoTardias { get; set; }
    public int AusentismoInjustificadas { get; set; }
    public int AusentismoJustificadas { get; set; }
    public string? Observaciones { get; set; }

    public string Condicion => NotaObtenida.HasValue
        ? (NotaObtenida >= NotaMinima ? "Aprobado" : "Reprobado")
        : "Pendiente";

    public bool Aprobado => NotaObtenida.HasValue && NotaObtenida >= NotaMinima;
    public int TotalAusentismo => AusentismoTardias + AusentismoInjustificadas + AusentismoJustificadas;

    // NavegaciÃ³n
    public Estudiante Estudiante { get; set; } = null!;
    public Asignatura Asignatura { get; set; } = null!;
    public Seccion Seccion { get; set; } = null!;
    public Periodo Periodo { get; set; } = null!;
}

