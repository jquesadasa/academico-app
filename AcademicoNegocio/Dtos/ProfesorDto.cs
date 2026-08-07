using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record ProfesorDto(
    Guid Id,
    string Especialidad,
    string? Nombre,
    string? PrimerApellido,
    string? SegundoApellido,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string NombreCompleto) : IIdentifiableDto;

