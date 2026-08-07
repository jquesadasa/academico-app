namespace AcademicoDominio.Entities;

public class Rol : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }

    // Navegación — many-to-many
    public ICollection<Permiso> Permisos { get; set; } = [];
}
