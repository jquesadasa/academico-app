namespace AcademicoDominio.Entities;

public class Nivel : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public int Orden { get; set; }

    // Navegación
    public ICollection<Seccion> Secciones { get; set; } = [];
}
