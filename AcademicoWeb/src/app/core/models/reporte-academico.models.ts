export interface ReporteConsolidado {
  readonly seccionId: number;
  readonly seccionCodigo: string;
  readonly periodoId: number;
  readonly periodoNombre: string;
  readonly generadoEn: string;
  readonly estudiantes: readonly ReporteConsolidadoEstudiante[];
}

export interface ReporteConsolidadoEstudiante {
  readonly estudianteId: number;
  readonly cedula: string;
  readonly nombreCompleto: string;
  readonly numeroLista: number | null;
  readonly monografia: MonografiaResumen | null;
  readonly teoriaConocimiento: TeoriaConocimientoResumen | null;
  readonly evaluacionesBI: readonly EvaluacionBIResumen[];
  readonly evaluacionesNacionales: readonly EvaluacionNacionalResumen[];
}

export interface MonografiaResumen {
  readonly areaInvestigacion: string | null;
  readonly supervisorNombre: string | null;
  readonly bandaAlcanzada: number | null;
  readonly observaciones: string | null;
}

export interface TeoriaConocimientoResumen {
  readonly bandaAlcanzada: string | null;
  readonly ausentismoExhibicion: number;
  readonly ausentismoOralidad: number;
  readonly observacionesExhibicion: string | null;
  readonly observacionesArgumentos: string | null;
  readonly observacionesOralidad: string | null;
  readonly observacionesEscritura: string | null;
}

export interface EvaluacionBIResumen {
  readonly asignatura: string;
  readonly bandaMinima: number;
  readonly bandaAlcanzada: number | null;
  readonly ausentismoTardias: number;
  readonly ausentismoInjustificadas: number;
  readonly ausentismoJustificadas: number;
  readonly observaciones: string | null;
  readonly aprobado: boolean;
}

export interface EvaluacionNacionalResumen {
  readonly asignatura: string;
  readonly notaMinima: number;
  readonly notaObtenida: number | null;
  readonly notaPruebaEstandarizada: number | null;
  readonly ausentismoTardias: number;
  readonly ausentismoInjustificadas: number;
  readonly ausentismoJustificadas: number;
  readonly observaciones: string | null;
  readonly condicion: string;
  readonly aprobado: boolean;
}
