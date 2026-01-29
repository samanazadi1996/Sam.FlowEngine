import { ErrorInterface } from './error-interface';
import { GetUserProjectsResponseInterface } from './get-user-projects-response-interface';

export interface GetUserProjectsResponseListBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: GetUserProjectsResponseInterface[];

}
