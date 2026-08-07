namespace AcademicoDominio.Entities;

public class Asignatura
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? TipoEvaluacion { get; set; } // "BI" | "Nacional"
    public bool Activo { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public bool EsBI => TipoEvaluacion == "BI";
    public bool EsNacional => TipoEvaluacion == "Nacional";

    // NavegaciÃ³n
    public ICollection<Seccion> Secciones { get; set; } = [];
    public ICollection<EvaluacionBI> EvaluacionesBI { get; set; } = [];
    public ICollection<EvaluacionNacional> EvaluacionesNacionales { get; set; } = [];
}

