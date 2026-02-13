import { ErrorInterface } from './error-interface';
import { ProjectJobInterface } from './project-job-interface';

export interface ProjectJobListBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: ProjectJobInterface[];

}
