namespace AcademicoDominio.Entities;

public class ProgramaAcademico : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? Codigo { get; set; }

    // Navegación
    public ICollection<Seccion> Secciones { get; set; } = [];
}
