import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing-module';
import { Dashboard } from './pages/dashboard/dashboard';
import { Projects } from './pages/projects/projects';
import { ProjectDetail } from './pages/projects/project-detail/project-detail';

@NgModule({
  declarations: [
    Dashboard,
    Projects,
    ProjectDetail
  ],
  imports: [
    CommonModule,
    MainRoutingModule
  ]
})
export class MainModule { }
