namespace AcademicoNegocio.Dtos;

public sealed record ReporteConsolidadoDto(
    Guid SeccionId,
    string SeccionCodigo,
    Guid PeriodoId,
    string PeriodoNombre,
    DateTime GeneradoEn,
    IReadOnlyList<ReporteConsolidadoEstudianteDto> Estudiantes);

public sealed record ReporteConsolidadoEstudianteDto(
    Guid EstudianteId,
    string Cedula,
    string NombreCompleto,
    int? NumeroLista,
    MonografiaResumenDto? Monografia,
    TeoriaConocimientoResumenDto? TeoriaConocimiento,
    IReadOnlyList<EvaluacionBIResumenDto> EvaluacionesBI,
    IReadOnlyList<EvaluacionNacionalResumenDto> EvaluacionesNacionales);

public sealed record MonografiaResumenDto(
    string? AreaInvestigacion,
    string? SupervisorNombre,
    int? BandaAlcanzada,
    string? Observaciones);

public sealed record TeoriaConocimientoResumenDto(
    string? BandaAlcanzada,
    int AusentismoExhibicion,
    int AusentismoOralidad,
    string? ObservacionesExhibicion,
    string? ObservacionesArgumentos,
    string? ObservacionesOralidad,
    string? ObservacionesEscritura);

public sealed record EvaluacionBIResumenDto(
    string Asignatura,
    int BandaMinima,
    int? BandaAlcanzada,
    int AusentismoTardias,
    int AusentismoInjustificadas,
    int AusentismoJustificadas,
    string? Observaciones,
    bool Aprobado);

public sealed record EvaluacionNacionalResumenDto(
    string Asignatura,
    decimal NotaMinima,
    decimal? NotaObtenida,
    decimal? NotaPruebaEstandarizada,
    int AusentismoTardias,
    int AusentismoInjustificadas,
    int AusentismoJustificadas,
    string? Observaciones,
    string Condicion,
    bool Aprobado);
