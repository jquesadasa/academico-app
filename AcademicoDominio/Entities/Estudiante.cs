namespace AcademicoDominio.Entities;

public class Estudiante : BaseEntity
{
    public string Cedula { get; set; } = string.Empty;
    public string? Nombre { get; set; }
    public string? Iniciales { get; set; }
    public string? PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; }
    public int? NumeroLista { get; set; }

    public string NombreCompleto =>
        string.IsNullOrWhiteSpace(PrimerApellido) && string.IsNullOrWhiteSpace(SegundoApellido)
            ? (Nombre ?? string.Empty)
            : $"{Nombre} {PrimerApellido} {SegundoApellido}".Trim();

    // Navegación
    public ICollection<Matricula> Matriculas { get; set; } = [];
    public ICollection<EvaluacionBI> EvaluacionesBI { get; set; } = [];
    public ICollection<EvaluacionNacional> EvaluacionesNacionales { get; set; } = [];
    public ICollection<Monografia> Monografias { get; set; } = [];
    public ICollection<TeoriaConocimiento> TeoriasConocimiento { get; set; } = [];
}
