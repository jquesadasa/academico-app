using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record NivelDto(
    Guid Id,
    string Nombre,
    int Orden,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt) : IIdentifiableDto;

