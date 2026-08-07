namespace AcademicoDominio.Entities;

public class Seccion
{
    public Guid Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string? Nombre { get; set; }
    public Guid PeriodoId { get; set; }
    public Guid? NivelId { get; set; }
    public Guid? ProgramaAcademicoId { get; set; }
    public Guid? InstitucionId { get; set; }
    public Guid? ProfesorGuiaId { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // NavegaciÃ³n
    public Periodo Periodo { get; set; } = null!;
    public Nivel? Nivel { get; set; }
    public ProgramaAcademico? ProgramaAcademico { get; set; }
    public Institucion? Institucion { get; set; }
    public Profesor? ProfesorGuia { get; set; }
    public ICollection<Matricula> Matriculas { get; set; } = [];
    public ICollection<Asignatura> Asignaturas { get; set; } = [];
    public ICollection<EvaluacionBI> EvaluacionesBI { get; set; } = [];
    public ICollection<EvaluacionNacional> EvaluacionesNacionales { get; set; } = [];
    public ICollection<Monografia> Monografias { get; set; } = [];
    public ICollection<TeoriaConocimiento> TeoriasConocimiento { get; set; } = [];
}


