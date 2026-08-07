namespace AcademicoServicios.Contracts.V1;

public sealed record TeoriaConocimientoUpsertRequest(
    Guid EstudianteId,
    Guid SeccionId,
    Guid PeriodoId,
    string? BandaAlcanzada,
    int AusentismoExhibicion,
    int AusentismoOralidad,
    string? ObservacionesExhibicion,
    string? ObservacionesArgumentos,
    string? ObservacionesOralidad,
    string? ObservacionesEscritura,
    bool Activo);


