\# Manual Maestro de Diseño UI/UX y Arquitectura Frontend  
\> \*\*Guía para la Replicación de Estilos Premium, Componentes y Buenas Prácticas de Rendimiento en Proyectos Angular\*\*

\---

\#\# 📋 Índice  
1\. \[Sistema de Diseño y Tokens CSS (Light & Dark Theme)\](\#1-sistema-de-diseño-y-tokens-css)  
2\. \[Efectos Visuales Premium y Glassmorphic Design\](\#2-efectos-visuales-premium-y-glassmorphic-design)  
3\. \[Estandarización de Componentes de Interfaz (UI)\](\#3-estandarización-de-componentes-de-interfaz)  
   \- \[Tarjetas (Glass Cards & KPI Cards)\](\#tarjetas-glass-cards--kpi-cards)  
   \- \[Tablas de Datos (MatTable Estandarizada)\](\#tablas-de-datos-mattable-estandarizada)  
   \- \[Formularios, Campos e Inputs (MatFormField Hardening)\](\#formularios-campos-e-inputs)  
   \- \[Badges, Chips y Colores de Estado\](\#badges-chips-y-colores-de-estado)  
   \- \[Menú Lateral y Navegación Principal\](\#menú-lateral-y-navegación-principal)  
   \- \[Sistema de Notificaciones (Toasts / SnackBars)\](\#sistema-de-notificaciones-toasts--snackbars)  
4\. \[Experiencia de Usuario Interactiva (UX Features)\](\#4-experiencia-de-usuario-interactiva-ux-features)  
   \- \[Paleta de Comandos Global (Ctrl \+ K / Cmd \+ K)\](\#paleta-de-comandos-global-ctrl--k--cmd--k)  
   \- \[Carga Progresiva con Skeletons (Shimmer Effect)\](\#carga-progresiva-con-skeletons-shimmer-effect)  
   \- \[Drag & Drop y Previsualización de Archivos\](\#drag--drop-y-previsualización-de-archivos)  
   \- \[Feedback Auditivo Micro-interactivo\](\#feedback-auditivo-micro-interactivo)  
   \- \[Tour Guía de Usuario (Onboarding Service)\](\#tour-guía-de-usuario-onboarding-service)  
5\. \[Arquitectura Frontend, Rendimiento y Cero Fugas de Memoria\](\#5-arquitectura-frontend-rendimiento-y-cero-fugas-de-memoria)  
   \- \[Patrón de Cancelación de Suscripciones (Cero Memory Leaks)\](\#patrón-de-cancelación-de-suscripciones)  
   \- \[Protección de Llamadas HTTP con \`take(1)\`\](\#protección-de-llamadas-http-con-take1)  
   \- \[Prevención de Suscripciones Acumulativas dentro de Métodos\](\#prevención-de-suscripciones-acumulativas-dentro-de-métodos)  
   \- \[Control de Ciclos Infinitos con NgRx Store y FormGroups\](\#control-de-ciclos-infinitos-con-ngrx-store-y-formgroups)  
   \- \[Estrategia de Caché Local e Invalidación Selectiva\](\#estrategia-de-caché-local-e-invalidación-selectiva)

\---

\#\# 1\. Sistema de Diseño y Tokens CSS

Para lograr una apariencia moderna, coherente y adaptable a cualquier dispositivo, todos los colores, espacios y elevaciones deben gestionarse mediante \*\*Variables CSS (Tokens)\*\* definidas centralizadamente en \`src/styles.scss\`.

\#\#\# 🎨 1.1 Tipografía  
Se utiliza una jerarquía tipográfica dual:  
\- \*\*Títulos y Encabezados:\*\* \`Outfit\` (fuente sans-serif moderna, geométrica y con carácter).  
\- \*\*Cuerpo, Listas y Tablas:\*\* \`Inter\` (alta legibilidad en densidad de datos).  
\- \*\*Código / Tags:\*\* \`Roboto Mono\`.

\`\`\`scss  
/\* Configuración Global de Fuentes \*/  
:root {  
  \--body-font-family: 'Outfit', 'Inter', sans-serif \!important;  
  \--body-background-color: var(--bg-app) \!important;  
}

body {  
  font-family: 'Outfit', 'Inter', sans-serif \!important;  
  letter-spacing: \-0.01em \!important;  
}

h1, h2, h3, h4, h5, h6, .text-heading, .mat-card-title, .mat-mdc-card-title {  
  font-family: 'Outfit', 'Inter', sans-serif \!important;  
  font-weight: 700 \!important;  
}  
\`\`\`

\#\#\# 🌗 1.2 Paleta de Colores y Modo Claro / Oscuro

\#\#\#\# Tema Claro (\`:root\`, \`html:not(.theme-dark)\`)  
\`\`\`css  
:root {  
  \--bg-app: \#f1f5f9;  
  \--text-primary: \#0f172a;  
  \--text-secondary: \#475569;  
  \--text-muted: \#64748b;  
  \--text-muted-soft: \#94a3b8;  
    
  \--card-bg: \#ffffff;  
  \--card-border: rgba(15, 23, 42, 0.06);  
  \--card-shadow: 0 10px 25px \-5px rgba(15, 23, 42, 0.08), 0 4px 12px \-2px rgba(15, 23, 42, 0.03);  
  \--card-hover-border: rgba(99, 102, 241, 0.3);  
  \--card-hover-shadow: 0 20px 40px \-5px rgba(15, 23, 42, 0.12), 0 12px 20px \-8px rgba(15, 23, 42, 0.06);  
    
  \--border-color: \#e2e8f0;  
  \--border-color-soft: \#f8fafc;  
    
  \--indigo-500: \#6366f1;  
  \--indigo-600: \#4f46e5;  
  \--accent: var(--indigo-500);  
  \--accent-hover: var(--indigo-600);  
    
  \--state-success: \#10b981;  
  \--state-success-bg: rgba(16, 185, 129, 0.1);  
  \--state-warning: \#f59e0b;  
  \--state-warning-bg: rgba(245, 158, 11, 0.1);  
  \--state-danger: \#f43f5e;  
  \--state-danger-bg: rgba(244, 63, 94, 0.1);  
  \--state-secondary: \#8b5cf6;  
    
  \--table-header-bg: \#f8fafc;  
  \--table-row-even: \#f8fafc;  
  \--table-row-hover: rgba(99, 102, 241, 0.04);  
  \--table-text: \#334155;  
    
  \--sidebar-bg-grad: linear-gradient(180deg, \#ffffff 0%, \#f8fafc 100%);  
  \--sidebar-text: \#64748b;  
  \--sidebar-link-hover-bg: rgba(99, 102, 241, 0.06);  
  \--sidebar-link-hover-text: \#4f46e5;  
  \--menu-active-bg: linear-gradient(135deg, var(--indigo-500) 0%, var(--indigo-600) 100%);  
  \--menu-active-shadow: 0 4px 12px rgba(79, 70, 229, 0.3);  
}  
\`\`\`

\#\#\#\# Tema Oscuro (\`html.theme-dark\`)  
\`\`\`css  
html.theme-dark {  
  \--bg-app: \#080d16;  
  \--text-primary: \#f8fafc;  
  \--text-secondary: \#cbd5e1;  
  \--text-muted: \#94a3b8;  
  \--text-muted-soft: \#64748b;  
    
  \--card-bg: rgba(15, 23, 42, 0.45);  
  \--card-border: rgba(255, 255, 255, 0.07);  
  \--card-shadow: 0 8px 32px 0 rgba(0, 0, 0, 0.25);  
  \--card-hover-border: rgba(99, 102, 241, 0.45);  
  \--card-hover-shadow: 0 12px 28px rgba(0, 0, 0, 0.35);  
    
  \--border-color: rgba(255, 255, 255, 0.08);  
  \--border-color-soft: rgba(255, 255, 255, 0.04);  
    
  \--indigo-500: \#818cf8;  
  \--indigo-600: \#6366f1;  
  \--accent: var(--indigo-500);  
  \--accent-hover: var(--indigo-600);  
    
  \--state-success: \#34d399;  
  \--state-success-bg: rgba(52, 211, 153, 0.15);  
  \--state-warning: \#fbbf24;  
  \--state-warning-bg: rgba(251, 191, 36, 0.15);  
  \--state-danger: \#fb7185;  
  \--state-danger-bg: rgba(251, 113, 133, 0.15);  
    
  \--table-header-bg: \#111827;  
  \--table-row-even: rgba(255, 255, 255, 0.015);  
  \--table-row-hover: rgba(99, 102, 241, 0.08);  
  \--table-text: \#e2e8f0;  
    
  \--sidebar-bg-grad: linear-gradient(180deg, \#090d16 0%, \#030508 100%);  
  \--sidebar-text: \#8295b9;  
  \--sidebar-link-hover-bg: rgba(255, 255, 255, 0.04);  
  \--sidebar-link-hover-text: \#ffffff;  
}  
\`\`\`

\---

\#\# 2\. Efectos Visuales Premium y Glassmorphic Design

\#\#\# ✨ 2.1 Tarjetas Glassmorphic (\`.glass-card\`)  
Aplica un sutil desenfoque de fondo (\*backdrop blur\*), bordes translúcidos con degradado y un destello de luz (\*shine effect\*) en la animación \`hover\`.

\`\`\`scss  
.glass-card, .mat-card.glass-card, .mat-mdc-card.glass-card {  
  backdrop-filter: blur(12px) \!important;  
  \-webkit-backdrop-filter: blur(12px) \!important;  
  border: 1px solid transparent \!important;  
  background-image: linear-gradient(var(--card-bg), var(--card-bg)),   
                    linear-gradient(135deg, rgba(99, 102, 241, 0.1) 0%, rgba(139, 92, 246, 0.05) 100%) \!important;  
  background-origin: border-box \!important;  
  background-clip: padding-box, border-box \!important;  
  box-shadow: var(--card-shadow) \!important;  
  border-radius: 16px \!important;  
  padding: 1.5rem \!important;  
  transition: transform 0.3s cubic-bezier(0.2, 0.8, 0.2, 1),   
              box-shadow 0.3s cubic-bezier(0.2, 0.8, 0.2, 1),   
              border-color 0.3s ease \!important;  
  position: relative;  
  overflow: hidden;  
}

/\* Hover Lift \*/  
.glass-card:hover, .mat-card.glass-card:hover, .mat-mdc-card.glass-card:hover {  
  transform: translateY(-3px) \!important;  
  box-shadow: var(--card-hover-shadow) \!important;  
}

/\* Barrido de Luz en Hover \*/  
.glass-card::before {  
  content: '';  
  position: absolute;  
  top: 0;  
  left: \-150%;  
  width: 50%;  
  height: 100%;  
  background: linear-gradient(  
    to right,  
    rgba(255, 255, 255, 0\) 0%,  
    rgba(255, 255, 255, 0.1) 50%,  
    rgba(255, 255, 255, 0\) 100%  
  );  
  transform: skewX(-25deg);  
  transition: 0.8s cubic-bezier(0.2, 0.8, 0.2, 1);  
  pointer-events: none;  
  z-index: 1;  
}

.glass-card:hover::before {  
  left: 150%;  
}  
\`\`\`

\---

\#\# 3\. Estandarización de Componentes de Interfaz

\#\#\# 📊 3.1 Tablas de Datos (\`MatTable\` Estandarizada)  
Resuelve desbordamientos en pantallas pequeñas, agrega encabezados sticky, filas pares con color cebra y elevación suave al pasar el cursor.

\`\`\`html  
\<\!-- Wrapper Responsive con Scroll Doble (Horizontal y Vertical) \--\>  
\<div class="table-responsive-container"\>  
  \<div class="table-scroll-wrapper"\>  
    \<table mat-table \[dataSource\]="dataSource" matSort class="w-full"\>  
      \<\!-- Definición de Columnas \--\>  
      \<ng-container matColumnDef="numeroPJ"\>  
        \<th mat-header-cell \*matHeaderCellDef mat-sort-header\> Patrimonio / N° PJ \</th\>  
        \<td mat-cell \*matCellDef="let element"\> {{ element.numeroPJ || 'N/A' }} \</td\>  
      \</ng-container\>

      \<tr mat-header-row \*matHeaderRowDef="displayedColumns; sticky: true"\>\</tr\>  
      \<tr mat-row \*matRowDef="let row; columns: displayedColumns;"\>\</tr\>  
    \</table\>  
  \</div\>

  \<\!-- Paginador Flotante Estandarizado \--\>  
  \<mat-paginator \[pageSizeOptions\]="\[10, 25, 50, 100\]" showFirstLastButtons\>\</mat-paginator\>  
\</div\>  
\`\`\`

\`\`\`scss  
/\* Estilos Globales de Tablas \*/  
.mat-mdc-table, table\[mat-table\] {  
  background: var(--card-bg) \!important;  
  border: 1px solid var(--border-color) \!important;  
  border-radius: 12px \!important;  
  overflow: hidden \!important;  
  box-shadow: var(--card-shadow) \!important;  
}

.mat-mdc-header-row, tr\[mat-header-row\] {  
  background-color: var(--table-header-bg) \!important;  
  border-bottom: 1px solid var(--border-color) \!important;  
}

.mat-mdc-header-cell, th\[mat-header-cell\] {  
  color: var(--text-primary) \!important;  
  text-transform: uppercase \!important;  
  font-size: 0.72rem \!important;  
  font-weight: 700 \!important;  
  letter-spacing: 0.05em \!important;  
  font-family: 'Outfit', 'Inter', sans-serif \!important;  
  padding: 14px 16px \!important;  
}

.mat-mdc-row:nth-child(even), tr\[mat-row\]:nth-child(even) {  
  background-color: var(--table-row-even) \!important;  
}

.mat-mdc-row:hover, tr\[mat-row\]:hover {  
  background-color: var(--table-row-hover) \!important;  
}

.mat-mdc-cell, td\[mat-cell\] {  
  color: var(--table-text) \!important;  
  font-size: 0.85rem \!important;  
  font-weight: 500 \!important;  
  font-family: 'Inter', sans-serif \!important;  
  padding: 12px 16px \!important;  
}  
\`\`\`

\---

\#\#\# 📝 3.2 Formularios e Inputs (\`MatFormField Hardening\`)

\#\#\#\# Solución Definitiva a Etiquetas Recortadas (First Letter Cut-Off) y Asteriscos Dobles \`\*\*\`:  
1\. \*\*Remover asteriscos manuales (\`\*\`) del HTML:\*\* Angular Material los añade de forma dinámica en campos con \`Validators.required\`.  
2\. \*\*Aplicar \`overflow: visible\` y desfasar la muesca de los \`\<mat-select\>\`:\*\*

\`\`\`scss  
/\* Prevenir recorte de letras en las etiquetas flotantes de Angular Material \*/  
.mat-mdc-form-field .mat-mdc-floating-label,  
.mat-mdc-form-field .mdc-floating-label,  
.mat-mdc-form-field label {  
  overflow: visible \!important;  
  text-overflow: clip \!important;  
}

.mdc-notched-outline\_\_notch {  
  overflow: visible \!important;  
}

/\* Corregir desplazamiento a la izquierda y recorte del primer carácter en selectores mat-select \*/  
.mat-mdc-form-field:has(mat-select) .mat-mdc-floating-label,  
.mat-mdc-form-field:has(mat-select) .mdc-floating-label {  
  margin-left: 10px \!important;  
}

/\* Estandarizar color del texto en rojo únicamente para el asterisco/requerido \*/  
.mat-mdc-form-field.mat-form-field-required .mat-mdc-floating-label,  
.mat-mdc-form-field.mat-form-field-required .mdc-floating-label,  
.mat-mdc-form-field.mat-form-field-required mat-label {  
  color: var(--state-danger, \#f43f5e) \!important;  
}  
\`\`\`

\---

\#\#\# 🏷️ 3.3 Badges, Chips y Colores de Estado  
Clase \`.estado\` para badges con diseño de píldora y asignación semántica de colores basada en palabras clave.

\`\`\`html  
\<span class="estado" \[class\]="element.descEstadoActivo"\>  
  {{ element.descEstadoActivo }}  
\</span\>  
\`\`\`

\`\`\`scss  
.estado {  
  border-radius: 30px \!important;  
  padding: 6px 14px \!important;  
  font-size: 0.72rem \!important;  
  font-weight: 700 \!important;  
  letter-spacing: 0.04em \!important;  
  text-transform: uppercase \!important;  
  display: inline-flex \!important;  
  align-items: center \!important;  
  justify-content: center \!important;  
  gap: 4px \!important;  
  min-width: 95px \!important;  
  transition: all 0.2s ease \!important;  
}

/\* Estado Éxito / Operativo \*/  
.estado\[class\*="BUENO"\], .estado\[class\*="Bueno"\], .estado\[class\*="FINALIZADO"\], .estado\[class\*="NUEVO"\] {  
  background-color: var(--state-success-bg) \!important;  
  color: var(--state-success) \!important;  
  border: 1px solid rgba(16, 185, 129, 0.25) \!important;  
}

/\* Estado Advertencia / Mantenimiento \*/  
.estado\[class\*="REGULAR"\], .estado\[class\*="Regular"\], .estado\[class\*="REVISAR"\], .estado\[class\*="REPARACION"\] {  
  background-color: var(--state-warning-bg) \!important;  
  color: var(--state-warning) \!important;  
  border: 1px solid rgba(245, 158, 11, 0.25) \!important;  
}

/\* Estado Crítico / Baja \*/  
.estado\[class\*="MALO"\], .estado\[class\*="Malo"\], .estado\[class\*="ELIMINADO"\], .estado\[class\*="OBSOLETO"\], .estado\[class\*="BAJA"\] {  
  background-color: var(--state-danger-bg) \!important;  
  color: var(--state-danger) \!important;  
  border: 1px solid rgba(244, 63, 94, 0.25) \!important;  
}  
\`\`\`

\---

\#\# 4\. Experiencia de Usuario Interactiva (UX Features)

\#\#\# ⌨️ 4.1 Paleta de Comandos Global (\`Ctrl \+ K\` / \`Cmd \+ K\`)  
Permite al usuario buscar activos, oficinas o navegar instantáneamente desde cualquier vista.

\`\`\`typescript  
// keyboard-shortcuts.service.ts  
@Injectable({ providedIn: 'root' })  
export class KeyboardShortcutsService {  
  private readonly \_dialog \= inject(MatDialog);

  constructor() {  
    this.listenGlobalShortcuts();  
  }

  private listenGlobalShortcuts() {  
    window.addEventListener('keydown', (e: KeyboardEvent) \=\> {  
      if ((e.ctrlKey || e.metaKey) && e.key.toLowerCase() \=== 'k') {  
        e.preventDefault();  
        this.openCommandPalette();  
      }  
    });  
  }

  openCommandPalette() {  
    if (this.\_dialog.openDialogs.some(d \=\> d.componentInstance instanceof CommandPaletteComponent)) {  
      return;  
    }  
    this.\_dialog.open(CommandPaletteComponent, {  
      width: '640px',  
      maxWidth: '92vw',  
      panelClass: 'command-palette-dialog',  
      position: { top: '80px' }  
    });  
  }  
}  
\`\`\`

\---

\#\#\# 🔊 4.2 Feedback Auditivo Micro-interactivo  
Añade una respuesta sonora sutil a los clics interactivos para dar sensación de aplicación de escritorio nativa.

\`\`\`typescript  
// audio.service.ts  
@Injectable({ providedIn: 'root' })  
export class AudioService {  
  private audioCtx?: AudioContext;

  playClick() {  
    try {  
      if (\!this.audioCtx) {  
        this.audioCtx \= new (window.AudioContext || (window as any).webkitAudioContext)();  
      }  
      if (this.audioCtx.state \=== 'suspended') {  
        this.audioCtx.resume();  
      }  
      const osc \= this.audioCtx.createOscillator();  
      const gain \= this.audioCtx.createGain();  
        
      osc.type \= 'sine';  
      osc.frequency.setValueAtTime(800, this.audioCtx.currentTime);  
      osc.frequency.exponentialRampToValueAtTime(400, this.audioCtx.currentTime \+ 0.04);  
        
      gain.gain.setValueAtTime(0.05, this.audioCtx.currentTime);  
      gain.gain.exponentialRampToValueAtTime(0.001, this.audioCtx.currentTime \+ 0.04);  
        
      osc.connect(gain);  
      gain.connect(this.audioCtx.destination);  
        
      osc.start();  
      osc.stop(this.audioCtx.currentTime \+ 0.04);  
    } catch (e) {  
      // Ignorar restricciones de autoplay si la interacción aún no se ha dado  
    }  
  }  
}  
\`\`\`

Escuchador global en \`app.component.ts\`:  
\`\`\`typescript  
@HostListener('document:click', \['$event'\])  
onDocumentClick(event: MouseEvent) {  
  const target \= event.target as HTMLElement;  
  if (target && target.closest('button, a, mat-chip-option, \[role="button"\], .mat-mdc-tab-link')) {  
    this.\_audioService.playClick();  
  }  
}  
\`\`\`

\---

\#\# 5\. Arquitectura Frontend, Rendimiento y Cero Fugas de Memoria

\#\#\# 🛑 5.1 Patrón de Cancelación de Suscripciones (Memory Leak Prevention)  
Cualquier suscripción a Observables (\`Store\`, \`ActionsSubject\`, \`Router.events\`) en componentes de larga vida \*\*DEBE\*\* cancelarse obligatoriamente al destruirse el componente.

\`\`\`typescript  
@Component({ ... })  
export class MiComponente implements OnInit, OnDestroy {  
  // 1\. Declarar el Subject de destrucción  
  private readonly destroySubject$ \= new Subject\<void\>();

  ngOnInit() {  
    // 2\. Encadenar takeUntil en todas las suscripciones  
    this.store.select(selectActivo)  
      .pipe(takeUntil(this.destroySubject$))  
      .subscribe(data \=\> {  
        // Lógica del componente  
      });  
  }

  // 3\. Completar el Subject en ngOnDestroy  
  ngOnDestroy(): void {  
    this.destroySubject$.next();  
    this.destroySubject$.complete();  
  }  
}  
\`\`\`

\---

\#\#\# ⚡ 5.2 Protección de Llamadas HTTP con \`take(1)\`  
Las llamadas directas a servicios de API REST deben cerrarse automáticamente tras recibir la primera respuesta para evitar que observables persistentes queden escuchando indefinidamente.

\`\`\`typescript  
// CORRECTO:  
this.\_activoService.srvBuscarActivo({ idUsuario, idOficina })  
  .pipe(take(1))  
  .subscribe({  
    next: (res) \=\> { /\* procesar datos \*/ },  
    error: (err) \=\> { /\* manejar error \*/ }  
  });  
\`\`\`

\---

\#\#\# 🔒 5.3 Prevención de Suscripciones Acumulativas dentro de Métodos  
\*\*REGLA DE ORO:\*\* Nunca colocar un \`.subscribe()\` del \`Store\` o un \`Observable\` continuo dentro de una función o método que se llame repetidamente (como un evento de clic o un método \`llenarDatos(id)\`).

\`\`\`typescript  
// ❌ INCORRECTO (Crea una suscripción nueva cada vez que se llama al método):  
llenarDatos(id: number) {  
  this.store.dispatch(ObtenerActivoAction({ id }));  
  this.store.select(state \=\> state.appActivo.archivos)  
    .pipe(takeUntil(this.destroySubject$))  
    .subscribe(archivos \=\> { this.archivos \= archivos; });  
}

// ✅ CORRECTO (La suscripción se crea una sola vez en ngOnInit):  
ngOnInit() {  
  this.store.select(state \=\> state.appActivo.archivos)  
    .pipe(takeUntil(this.destroySubject$))  
    .subscribe(archivos \=\> { this.archivos \= archivos || \[\]; });  
}

llenarDatos(id: number) {  
  this.store.dispatch(ObtenerActivoAction({ id }));  
}  
\`\`\`

\---

\#\#\# 🛡️ 5.4 Control de Bucles Infinitos entre Formularios y NgRx Store  
Para evitar que la actualización de un formulario reactivo dispare un selector de NgRx y este re-llene el formulario en un bucle infinito de re-renderizado:

\`\`\`typescript  
private idActivoCargado: number | undefined;

ngOnInit() {  
  this.activo$.pipe(takeUntil(this.destroySubject$)).subscribe((f: ActivoState) \=\> {  
    if (typeof f.idActivo \!== 'undefined') {  
      // Guard de comparación: Solo cargar si el ID de activo cambió realmente  
      if (this.idActivoCargado \!== f.idActivo) {  
        this.idActivoCargado \= f.idActivo;  
        this.llenarDatos(f.idActivo);  
      }  
    } else {  
      this.idActivoCargado \= undefined;  
      this.limpiarFormularioCompleto();  
    }  
  });  
}  
\`\`\`

\---

\#\#\# 🧹 5.5 Estrategia de Caché e Invalidación Selectiva  
Para optimizar las peticiones de red y evitar llamadas duplicadas al navegar entre la grilla y el formulario de mantenimiento:

1\. \*\*Guardar resultados en la caché del servicio:\*\*  
\`\`\`typescript  
// Al recibir respuesta exitosa en la grilla:  
this.\_activoService.cacheList \= this.fullDataList;  
this.\_activoService.cacheOficinaId \= currentOficinaId || null;  
\`\`\`

2\. \*\*Excluir acciones de lectura en la invalidación de caché:\*\*  
\`\`\`typescript  
// Filtrar acciones que alteren estado (Modificar/Eliminar/Insertar), excluyendo lecturas:  
this.\_actions.pipe(  
  takeUntil(this.destroySubject$),  
  filter((action: any) \=\> {  
    const type \= action.type;  
    return (  
      type.startsWith('\[ACT\]') &&  
      type.endsWith('Success') &&  
      type \!== '\[ACT\]BuscarActivoSuccess' &&  
      type \!== '\[ACT\]ObtenerActivoSuccess' &&  
      type \!== '\[ACT\]ObtenerArchivoSuccess' &&  
      type \!== '\[ACT\]SeleccionarActivoSuccess'  
    );  
  })  
).subscribe(() \=\> {  
  this.detailsCache \= {}; // Solo invalidar la caché local cuando de verdad hubo una mutación de datos  
});  
\`\`\`

\---

\#\# 🎯 Lista de Chequeo para Nuevos Proyectos

\- \[ \] Incluir variables CSS \`:root\` y \`html.theme-dark\` en \`src/styles.scss\`.  
\- \[ \] Importar fuentes Google Fonts (\`Outfit\` e \`Inter\`).  
\- \[ \] Aplicar la clase \`.glass-card\` a todos los contenedores principales.  
\- \[ \] Envolver las tablas \`mat-table\` en \`.table-responsive-container\` y \`.table-scroll-wrapper\`.  
\- \[ \] Incluir los fixes globales para \`mat-form-field\` (\`overflow: visible\` y \`:has(mat-select)\` margin shift).  
\- \[ \] Verificar que todo componente con suscripciones implemente \`OnDestroy\` y \`takeUntil(destroySubject$)\`.  
\- \[ \] Asegurar que las peticiones REST individuales usen \`.pipe(take(1))\`.  
\- \[ \] Probar la navegación con teclado y verificar que el \`focus-visible\` aplique el anillo azul/índigo animado.

