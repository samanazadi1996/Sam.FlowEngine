import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { SharedModule } from './core/shared/shared.module';

const routes: Routes = [
  { path: '', redirectTo: 'main', pathMatch: 'full' },
  {
    path: 'main',
    loadChildren: () => import('./modules/main/main-module').then(m => m.MainModule),
    canActivate: [AuthGuard]

  },
  {
    path: 'account',
    loadChildren: () => import('./modules/account/account-module').then(m => m.AccountModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes),SharedModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
