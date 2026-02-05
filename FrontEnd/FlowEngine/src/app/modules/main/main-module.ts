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
import { JobStartForm } from './pages/projects/project-details/update-job/job-start-form/job-start-form';
import { JobRandomForm } from './pages/projects/project-details/update-job/job-random-form/job-random-form';
import { JobSleepForm } from './pages/projects/project-details/update-job/job-sleep-form/job-sleep-form';
import { JobHttpRequestForm } from './pages/projects/project-details/update-job/job-http-request-form/job-http-request-form';
import { JobTimerForm } from './pages/projects/project-details/update-job/job-timer-form/job-timer-form';
import { JobIfForm } from './pages/projects/project-details/update-job/job-if-form/job-if-form';

@NgModule({
  declarations: [
    Dashboard,
    Projects,
    ProjectDetails,
    CreateProject,
    UpdateJob,
    CreateJob,
    JobStartForm,
    JobRandomForm,
    JobSleepForm,
    JobHttpRequestForm,
    JobTimerForm,
    JobIfForm,
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    FormsModule,
    SharedModule
  ]
})
export class MainModule { }
