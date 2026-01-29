import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Main } from './main';
import { Dashboard } from './pages/dashboard/dashboard';
import { Projects } from './pages/projects/projects';
import { ProjectDetail } from './pages/projects/project-detail/project-detail';

const routes: Routes = [
  {
    path: '',
    component: Main,
    children: [
      // { path: '', component: Main },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: Dashboard },
      { path: 'projects', component: Projects },
      { path: 'projects/:projectName', component: ProjectDetail },
      // { path: 'profile/:userName', component: ProfileComponent },
      // { path: 'profile', component: ProfileComponent },
      // { path: 'settings', component: SettingsComponent },
      // { path: 'top-users', component: TopPlayersComponent },
    ]
  }

];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
