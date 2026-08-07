namespace AcademicoServicios.Contracts.V1;

public sealed record EvaluacionNacionalUpsertRequest(
    Guid EstudianteId,
    Guid AsignaturaId,
    Guid SeccionId,
    Guid PeriodoId,
    decimal NotaMinima,
    decimal? NotaObtenida,
    decimal? NotaPruebaEstandarizada,
    int AusentismoTardias,
    int AusentismoInjustificadas,
    int AusentismoJustificadas,
    string? Observaciones,
    bool Activo);


