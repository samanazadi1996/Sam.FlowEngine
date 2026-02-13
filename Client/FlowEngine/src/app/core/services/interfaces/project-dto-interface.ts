import { ProjectJobDtoInterface } from './project-job-dto-interface';

export interface ProjectDtoInterface {

  id: number;
  projectName?: string;
  jobs?: ProjectJobDtoInterface[];
  started: boolean;

}
