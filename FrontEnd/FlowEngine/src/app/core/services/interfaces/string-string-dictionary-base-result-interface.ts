import { ErrorInterface } from './error-interface';

export interface StringStringDictionaryBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: any;

}
