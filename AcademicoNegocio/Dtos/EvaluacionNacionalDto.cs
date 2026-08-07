using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record EvaluacionNacionalDto(
    Guid Id,
    Guid EstudianteId,
    Guid AsignaturaId,
    Guid SeccionId,
    Guid PeriodoId,
    decimal NotaMinima,
    decimal? NotaObtenida,
    decimal? NotaPruebaEstandarizada,
    int AusentismoTardias,
    int AusentismoInjustificadas,
    int AusentismoJustificadas,
    string? Observaciones,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string Condicion,
    bool Aprobado,
    int TotalAusentismo,
    string? EstudianteNombreCompleto,
    string? AsignaturaNombre) : IIdentifiableDto;


