import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing-module';
import { Dashboard } from './pages/dashboard/dashboard';
import { Projects } from './pages/projects/projects';
import { ProjectDetails } from './pages/projects/project-details/project-details';
import { CreateProject } from './pages/projects/create-project/create-project';
import { FormsModule } from '@angular/forms';
import { UpdateJob } from './pages/projects/project-details/update-job/update-job';
import { SharedModule } from '../../core/shared/shared.module';
import { CreateJob } from './pages/projects/project-details/create-job/create-job';

@NgModule({
  declarations: [
    Dashboard,
    Projects,
    ProjectDetails,
    CreateProject,
    UpdateJob,
    CreateJob,
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    FormsModule,
    SharedModule
  ]
})
export class MainModule { }
