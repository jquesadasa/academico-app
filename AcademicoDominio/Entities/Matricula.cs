namespace AcademicoDominio.Entities;

public class Matricula
{
    public Guid Id { get; set; }
    public Guid EstudianteId { get; set; }
    public Guid SeccionId { get; set; }
    public string? Estado { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // NavegaciÃ³n
    public Estudiante Estudiante { get; set; } = null!;
    public Seccion Seccion { get; set; } = null!;
}


