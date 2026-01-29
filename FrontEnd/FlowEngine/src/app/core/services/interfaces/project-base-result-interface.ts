import { ErrorInterface } from './error-interface';
import { ProjectInterface } from './project-interface';

export interface ProjectBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: ProjectInterface;

}
