import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { OpenApiDiscoveryService } from '../services/openapi-discovery.service';

export const appReadyGuard: CanActivateFn = async () => {
  const discoveryService = inject(OpenApiDiscoveryService);
  await discoveryService.load();
  return true;
};
