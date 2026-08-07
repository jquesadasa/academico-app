namespace AcademicoDominio.Entities;

public class Auditoria
{
    public Guid Id { get; set; }
    public string? Accion { get; set; }
    public string? UsuarioId { get; set; }
    public string? Tabla { get; set; }
    public Guid? RegistroId { get; set; }
    public string? DatosAnteriores { get; set; }
    public string? DatosNuevos { get; set; }
    public string? IpAddress { get; set; }
    public DateTime FechaAccion { get; set; } = DateTime.UtcNow;
}


