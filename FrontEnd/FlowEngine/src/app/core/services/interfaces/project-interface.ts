import { ProjectJobInterface } from './project-job-interface';

export interface ProjectInterface {

  id: number;
  createdBy: string;
  created: string;
  lastModifiedBy?: string;
  lastModified?: string;
  projectName?: string;
  projectJobs?: ProjectJobInterface[];
  started: boolean;

}
