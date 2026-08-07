using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record AsignaturaDto(
    Guid Id,
    string Nombre,
    string? TipoEvaluacion,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool EsBI,
    bool EsNacional) : IIdentifiableDto;

