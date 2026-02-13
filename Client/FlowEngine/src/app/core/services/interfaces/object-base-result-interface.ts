import { ErrorInterface } from './error-interface';

export interface ObjectBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: any;

}
