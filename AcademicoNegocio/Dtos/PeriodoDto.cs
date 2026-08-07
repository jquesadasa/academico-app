using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record PeriodoDto(
    Guid Id,
    string Nombre,
    DateTime FechaInicio,
    DateTime FechaFin,
    int Anio,
    string? Estado,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt) : IIdentifiableDto;

