namespace AcademicoDominio.Entities;

public class Institucion : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Codigo { get; set; }
    public Guid? DireccionRegionalId { get; set; }

    // NavegaciÃ³n
    public DireccionRegional? DireccionRegional { get; set; }
    public ICollection<Seccion> Secciones { get; set; } = [];
}

