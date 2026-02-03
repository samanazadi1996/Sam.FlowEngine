import { ErrorInterface } from './error-interface';
import { IdTitleDtoInterface } from './id-title-dto-interface';

export interface IdTitleDtoListBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data?: IdTitleDtoInterface[];

}
