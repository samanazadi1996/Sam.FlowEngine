import { ErrorInterface } from './error-interface';
import { ProjectJobDtoInterface } from './project-job-dto-interface';

export interface ProjectJobDtoBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: ProjectJobDtoInterface;

}
