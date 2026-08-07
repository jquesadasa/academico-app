using AcademicoNegocio.Abstractions;

namespace AcademicoNegocio.Dtos;

public sealed record SeccionDto(
    Guid Id,
    string Codigo,
    string? Nombre,
    Guid PeriodoId,
    Guid? NivelId,
    Guid? ProgramaAcademicoId,
    Guid? InstitucionId,
    Guid? ProfesorGuiaId,
    bool Activo,
    DateTime CreatedAt) : IIdentifiableDto;


