import { ErrorInterface } from './error-interface';
import { AuthenticationResponseInterface } from './authentication-response-interface';

export interface AuthenticationResponseBaseResultInterface {

  success: boolean;
  errors?: ErrorInterface[];
  data: AuthenticationResponseInterface;

}
