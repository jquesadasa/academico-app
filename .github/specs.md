# Especificación de Requerimientos de Software

## Sistema de Gestión de Reportes de Bandas BI y Evaluación Académica

**Versión:** 1.0  
**Fuente principal:** REPORTE DE BANDAS QUINTO II SEM24.xlsx  
**Fuente secundaria:** Documento de Requerimientos del Software.docx  
**Periodo analizado:** Segundo Semestre 2024  
**Institución:** Instituto de Educación Dr. Clodomiro Picado Twight  
**Programa:** Bachillerato Internacional

---

# 1. Resumen Ejecutivo

El sistema requerido debe reemplazar una plantilla Excel utilizada para gestionar reportes académicos del Bachillerato Internacional y asignaturas nacionales.

El Excel actual funciona como un sistema semi automatizado que integra:

- Menú de navegación
- Listas de estudiantes
- Carga de notas por asignatura
- Componentes especiales como Monografía y Teoría del Conocimiento
- Consolidado grupal
- Informes individuales con formato institucional y ministerial

---

# 2. Alcance

## 2.1 Alcance Funcional

El sistema debe permitir:

- Registrar instituciones, periodos lectivos, niveles y secciones.
- Administrar listas oficiales de estudiantes por sección.
- Registrar docentes por asignatura, sección y componente.
- Gestionar asignaturas BI evaluadas por bandas.
- Gestionar asignaturas nacionales evaluadas por nota porcentual.
- Registrar Monografía por estudiante.
- Registrar Teoría del Conocimiento por estudiante.
- Registrar ausentismo por asignatura y componente.
- Registrar observaciones cualitativas extensas.
- Generar consolidado grupal en formato tipo sábana.
- Generar informe individual de rendimiento.
- Generar reporte específico de Monografía.
- Generar reporte específico de Teoría del Conocimiento.
- Exportar reportes a PDF y Excel.
- Controlar roles, permisos, auditoría y trazabilidad.

## 2.2 Fuera de Alcance Inicial

- Integración automática con plataformas oficiales MEP.
- Cálculo automático oficial de resultados BI internacionales.
- Gestión completa de matrícula institucional anual fuera del programa BI.
- Comunicación masiva con familias mediante correo o mensajería.
- Firma digital avanzada, salvo que se defina como requerimiento posterior.

---

# 3. Fase 1: Ingeniería Inversa del Excel

## 3.1 Hojas Identificadas

| Hoja | Función Inferida |
|--------|--------|
| MENÚ | Navegación principal del sistema Excel |
| Principal | Pantalla principal o dashboard de acceso a secciones y módulos |
| Lista | Fuente maestra de estudiantes por sección, con nombre, cédula e iniciales |
| Informe | Plantilla de reporte individual por estudiante |
| Grupo | Consolidado completo por sección |
| MONOGRAFIA | Reporte individualizado de avance en Monografía |
| TEORIACONOCIMIENTO | Registro y reporte grupal de Teoría del Conocimiento |
| ASIGNATURA | Registro por asignatura, docente, sección, bandas, notas, ausentismo y observaciones |
| Hoja1 | Hoja residual o no funcional |

El Excel contiene navegación interna mediante referencias a secciones específicas, reportes preformateados, bloques repetidos por sección y dependencias entre hojas como:

- Lista
- Grupo
- Informe
- MONOGRAFIA
- TEORIACONOCIMIENTO
- ASIGNATURA

## 3.2 Módulos del Sistema Inferidos

- Menú y navegación
- Gestión institucional
- Gestión de periodos lectivos
- Gestión de secciones
- Gestión de estudiantes
- Gestión de docentes
- Gestión de asignaturas
- Evaluación por bandas BI
- Evaluación nacional porcentual
- Teoría del Conocimiento
- Monografía
- Ausentismo
- Observaciones cualitativas
- Consolidado grupal
- Informe individual
- Reportes por asignatura
- Exportación
- Auditoría y trazabilidad
- Seguridad y roles
- Configuración de catálogos

## 3.3 Entidades de Negocio Identificadas

| Entidad | Evidencia / Uso |
|----------|----------|
| Institución | Encabezados oficiales del MEP y nombre del instituto |
| Dirección Regional | Dirección Regional de Educación Turrialba |
| Periodo Lectivo | Segundo Semestre 2024 |
| Programa Académico | Bachillerato Internacional |
| Nivel | Undécimo Año |
| Sección | 11-1 y 11-2 |
| Estudiante | Nombre, cédula, número de lista e iniciales |
| Docente | Asociado a asignatura o componente |
| Profesor Guía | Firma del informe individual |
| Supervisor de Monografía | Asociado al área y estudiante |
| Asignatura | Historia, Lengua A, Lengua B, Sociedad Digital, Matemática AI, Biología, Estudios Sociales, Cívica |
| Componente BI | Monografía y Teoría del Conocimiento |
| Evaluación BI | Banda mínima, banda alcanzada, ausentismo, observaciones |
| Evaluación Nacional | Nota mínima, nota obtenida, condición, prueba estandarizada |
| Ausentismo | Tardías, injustificadas y justificadas |
| Reporte | Individual, grupal, asignatura, Monografía, TdC |

---

# 3.4 Procesos Operativos Inferidos

## Creación o actualización de lista oficial

- Se registra cada estudiante por sección.
- Se almacena número de lista, nombre y cédula.
- La lista alimenta los reportes posteriores.

## Carga por asignatura

- Cada docente registra banda mínima o nota mínima.
- Registra banda alcanzada o nota alcanzada.
- Registra ausencias.
- Registra observación descriptiva.

## Carga de Teoría del Conocimiento

- Se registra banda alcanzada en escala A, B, C, D, E.
- Se registra ausentismo.
- Se registran observaciones sobre exhibición, argumentos, oralidad y escritura.

## Carga de Monografía

- Se registra área de investigación.
- Se registra supervisor.
- Se registra avance u observación del trabajo realizado.