using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record EstudianteDto(
    Guid Id,
    string Cedula,
    string? Nombre,
    string? PrimerApellido,
    string? SegundoApellido,
    int? NumeroLista,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string NombreCompleto) : IIdentifiableDto;

