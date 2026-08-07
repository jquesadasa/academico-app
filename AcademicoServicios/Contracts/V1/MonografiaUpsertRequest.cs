namespace AcademicoServicios.Contracts.V1;

public sealed record MonografiaUpsertRequest(
    Guid EstudianteId,
    Guid SeccionId,
    Guid PeriodoId,
    string? AreaInvestigacion,
    string? SupervisorNombre,
    Guid? SupervisorId,
    int? BandaAlcanzada,
    string? Observaciones,
    bool Activo);


