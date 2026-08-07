using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record InstitucionDto(
    Guid Id,
    string Nombre,
    string? Codigo,
    Guid? DireccionRegionalId,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt) : IIdentifiableDto;


