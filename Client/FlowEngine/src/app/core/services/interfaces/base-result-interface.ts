import { ErrorInterface } from './error-interface';

export interface BaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];

}
