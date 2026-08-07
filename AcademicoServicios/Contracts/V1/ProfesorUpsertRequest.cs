namespace AcademicoServicios.Contracts.V1;

public sealed record ProfesorUpsertRequest(
    string Especialidad,
    string? Nombre,
    string? PrimerApellido,
    string? SegundoApellido,
    bool Activo);