using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record TeoriaConocimientoDto(
    Guid Id,
    Guid EstudianteId,
    Guid SeccionId,
    Guid PeriodoId,
    string? BandaAlcanzada,
    int AusentismoExhibicion,
    int AusentismoOralidad,
    string? ObservacionesExhibicion,
    string? ObservacionesArgumentos,
    string? ObservacionesOralidad,
    string? ObservacionesEscritura,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? EstudianteNombreCompleto) : IIdentifiableDto;


