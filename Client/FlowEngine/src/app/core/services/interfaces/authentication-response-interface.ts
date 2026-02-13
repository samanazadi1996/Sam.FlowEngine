

export interface AuthenticationResponseInterface {

  id?: string;
  userName?: string;
  email?: string;
  roles?: string[];
  isVerified: boolean;
  jwToken?: string;

}
