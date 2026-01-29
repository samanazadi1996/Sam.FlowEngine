import { ErrorCodeInterface } from './error-code-interface';

export interface ErrorInterface {

  errorCode: ErrorCodeInterface;
  fieldName?: string;
  description?: string;

}
