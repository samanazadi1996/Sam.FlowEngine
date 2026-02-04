import { ErrorInterface } from './error-interface';
import { ProjectJobDtoInterface } from './project-job-dto-interface';

export interface ProjectJobDtoListBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: ProjectJobDtoInterface[];

}
