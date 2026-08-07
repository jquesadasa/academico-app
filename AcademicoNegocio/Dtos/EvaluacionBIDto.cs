using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record EvaluacionBIDto(
    Guid Id,
    Guid EstudianteId,
    Guid AsignaturaId,
    Guid SeccionId,
    Guid PeriodoId,
    int BandaMinima,
    int? BandaAlcanzada,
    int AusentismoTardias,
    int AusentismoInjustificadas,
    int AusentismoJustificadas,
    string? Observaciones,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool Aprobado,
    int TotalAusentismo,
    string? EstudianteNombreCompleto,
    string? AsignaturaNombre) : IIdentifiableDto;


