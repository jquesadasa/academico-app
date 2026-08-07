namespace AcademicoDominio.Entities;

/// <summary>
/// Componente de MonografÃ­a del Bachillerato Internacional.
/// </summary>
public class Monografia : BaseEntity
{
    public Guid EstudianteId { get; set; }
    public Guid SeccionId { get; set; }
    public Guid PeriodoId { get; set; }
    public string? AreaInvestigacion { get; set; }
    public string? SupervisorNombre { get; set; }
    public Guid? SupervisorId { get; set; }
    public int? BandaAlcanzada { get; set; }
    public string? Observaciones { get; set; }

    // NavegaciÃ³n
    public Estudiante Estudiante { get; set; } = null!;
    public Seccion Seccion { get; set; } = null!;
    public Periodo Periodo { get; set; } = null!;
    public Profesor? Supervisor { get; set; }
}

