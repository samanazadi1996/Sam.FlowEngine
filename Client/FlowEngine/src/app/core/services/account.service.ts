// Auto-generated service for tag: Account
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { AuthenticateCommandInterface } from './interfaces/authenticate-command-interface';
import { AuthenticationResponseBaseResultInterface } from './interfaces/authentication-response-base-result-interface';
import { ChangeUserNameCommandInterface } from './interfaces/change-user-name-command-interface';
import { BaseResultInterface } from './interfaces/base-result-interface';
import { ChangePasswordCommandInterface } from './interfaces/change-password-command-interface';

@Injectable({ providedIn: 'root' })
export class AccountService {
    constructor(private http: HttpClient) { }


    postApiAccountAuthenticate(body : AuthenticateCommandInterface) {
        return this.http.post<AuthenticationResponseBaseResultInterface>(`${environment.serverUrl}/api/Account/Authenticate`, body);
    }

    putApiAccountChangeUserName(body : ChangeUserNameCommandInterface) {
        return this.http.put<BaseResultInterface>(`${environment.serverUrl}/api/Account/ChangeUserName`, body);
    }

    putApiAccountChangePassword(body : ChangePasswordCommandInterface) {
        return this.http.put<BaseResultInterface>(`${environment.serverUrl}/api/Account/ChangePassword`, body);
    }

    postApiAccountStart() {
        return this.http.post<AuthenticationResponseBaseResultInterface>(`${environment.serverUrl}/api/Account/Start`, { });
    }

}
