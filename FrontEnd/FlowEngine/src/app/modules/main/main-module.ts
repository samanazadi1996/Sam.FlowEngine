import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing-module';
import { Dashboard } from './pages/dashboard/dashboard';
import { Projects } from './pages/projects/projects';
import { ProjectDetail } from './pages/projects/project-detail/project-detail';
import { CreateProject } from './pages/projects/create-project/create-project';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    Dashboard,
    Projects,
    ProjectDetail,
    CreateProject
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    FormsModule

  ]
})
export class MainModule { }
