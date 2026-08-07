namespace AcademicoDominio.Entities;

public class DireccionRegional : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Codigo { get; set; }

    // Navegación
    public ICollection<Institucion> Instituciones { get; set; } = [];
}
