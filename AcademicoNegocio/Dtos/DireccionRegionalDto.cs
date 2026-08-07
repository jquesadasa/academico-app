using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record DireccionRegionalDto(
    Guid Id,
    string Nombre,
    string? Codigo,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt) : IIdentifiableDto;

