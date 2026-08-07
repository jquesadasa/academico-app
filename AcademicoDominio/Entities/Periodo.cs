namespace AcademicoDominio.Entities;

public class Periodo : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int Anio { get; set; }
    public string? Estado { get; set; }

    // Navegación
    public ICollection<Seccion> Secciones { get; set; } = [];
}
