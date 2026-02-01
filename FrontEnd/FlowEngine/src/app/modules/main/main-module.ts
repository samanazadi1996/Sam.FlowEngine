import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing-module';
import { Dashboard } from './pages/dashboard/dashboard';
import { Projects } from './pages/projects/projects';
import { ProjectDetails } from './pages/projects/project-details/project-details';
import { CreateProject } from './pages/projects/create-project/create-project';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    Dashboard,
    Projects,
    ProjectDetails,
    CreateProject
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    FormsModule

  ]
})
export class MainModule { }
