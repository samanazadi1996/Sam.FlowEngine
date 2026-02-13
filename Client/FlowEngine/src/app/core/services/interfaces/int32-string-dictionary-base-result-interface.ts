import { ErrorInterface } from './error-interface';

export interface Int32StringDictionaryBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: any;

}
