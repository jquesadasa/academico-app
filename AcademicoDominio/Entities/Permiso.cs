namespace AcademicoDominio.Entities;

public class Permiso : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? Modulo { get; set; }

    // Navegación — many-to-many
    public ICollection<Rol> Roles { get; set; } = [];
}
