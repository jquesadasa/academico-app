import { Routes } from '@angular/router';
import { appReadyGuard } from './core/guards/app-ready.guard';

export const routes: Routes = [
	{
		path: '',
		canActivate: [appReadyGuard],
		loadComponent: () => import('./layout/shell.component').then((m) => m.ShellComponent),
		children: [
			{
				path: 'dashboard',
				loadComponent: () => import('./features/dashboard/dashboard.component').then((m) => m.DashboardComponent)
			},
			{
				path: 'mantenimientos/:entity',
				loadComponent: () => import('./features/maintenance/maintenance-page.component').then((m) => m.MaintenancePageComponent)
			},
			{
				path: 'reportes/bandas',
				loadComponent: () => import('./features/reports/bandas-report.component').then((m) => m.BandasReportComponent)
			},
			{
				path: '',
				pathMatch: 'full',
				redirectTo: 'dashboard'
			}
		]
	},
	{
		path: '**',
		redirectTo: ''
	}
];
