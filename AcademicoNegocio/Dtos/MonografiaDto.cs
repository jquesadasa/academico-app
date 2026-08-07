using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record MonografiaDto(
    Guid Id,
    Guid EstudianteId,
    Guid SeccionId,
    Guid PeriodoId,
    string? AreaInvestigacion,
    string? SupervisorNombre,
    Guid? SupervisorId,
    int? BandaAlcanzada,
    string? Observaciones,
    bool Activo,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? EstudianteNombreCompleto) : IIdentifiableDto;


