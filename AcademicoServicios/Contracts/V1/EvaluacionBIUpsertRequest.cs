namespace AcademicoServicios.Contracts.V1;

public sealed record EvaluacionBIUpsertRequest(
    Guid EstudianteId,
    Guid AsignaturaId,
    Guid SeccionId,
    Guid PeriodoId,
    int BandaMinima,
    int? BandaAlcanzada,
    int AusentismoTardias,
    int AusentismoInjustificadas,
    int AusentismoJustificadas,
    string? Observaciones,
    bool Activo);


