using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record ProgramaAcademicoDto(
    Guid Id,
    string Nombre,
    string? Descripcion,
    string? Codigo,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt) : IIdentifiableDto;

