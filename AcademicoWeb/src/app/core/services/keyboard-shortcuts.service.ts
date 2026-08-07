import { Injectable, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CommandPaletteComponent } from '../components/command-palette.component';

@Injectable({ providedIn: 'root' })
export class KeyboardShortcutsService {
  private readonly dialog = inject(MatDialog);

  constructor() {
    this.listenGlobalShortcuts();
  }

  openCommandPalette(): void {
    if (this.dialog.openDialogs.some((dialogRef) => dialogRef.componentInstance instanceof CommandPaletteComponent)) {
      return;
    }

    this.dialog.open(CommandPaletteComponent, {
      width: '640px',
      maxWidth: '92vw',
      panelClass: 'command-palette-dialog',
      position: { top: '80px' }
    });
  }

  private listenGlobalShortcuts(): void {
    window.addEventListener('keydown', (event: KeyboardEvent) => {
      if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === 'k') {
        event.preventDefault();
        this.openCommandPalette();
      }
    });
  }
}
