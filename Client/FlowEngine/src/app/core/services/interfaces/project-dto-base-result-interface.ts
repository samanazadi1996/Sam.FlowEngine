import { ErrorInterface } from './error-interface';
import { ProjectDtoInterface } from './project-dto-interface';

export interface ProjectDtoBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: ProjectDtoInterface;

}
