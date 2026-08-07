namespace AcademicoServicios.Contracts.V1;

public sealed record EstudianteUpsertRequest(
    string Cedula,
    string? Nombre,
    string? PrimerApellido,
    string? SegundoApellido,
    int? NumeroLista,
    bool Activo);