namespace AcademicoDominio.Entities;

public class Profesor : BaseEntity
{
    public string Especialidad { get; set; } = string.Empty;
    public string? Nombre { get; set; }
    public string? PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; }

    public string NombreCompleto =>
        $"{Nombre} {PrimerApellido} {SegundoApellido}".Trim();

    // Navegación
    public ICollection<Seccion> Secciones { get; set; } = [];
    public ICollection<Monografia> MonografiasSupervision { get; set; } = [];
}
