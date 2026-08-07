namespace AcademicoDominio.Entities;

public class Reporte
{
    public Guid Id { get; set; }
    public string? Tipo { get; set; }
    public Guid? EstudianteId { get; set; }
    public Guid? PeriodoId { get; set; }
    public Guid? SeccionId { get; set; }
    public DateTime? FechaGeneracion { get; set; }

    // NavegaciÃ³n
    public Estudiante? Estudiante { get; set; }
    public Periodo? Periodo { get; set; }
    public Seccion? Seccion { get; set; }
}


