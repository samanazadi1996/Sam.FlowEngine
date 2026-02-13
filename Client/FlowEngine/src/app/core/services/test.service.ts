// Auto-generated service for tag: Test
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { ObjectBaseResultInterface } from './interfaces/object-base-result-interface';

@Injectable({ providedIn: 'root' })
export class TestService {
    constructor(private http: HttpClient) { }


    getApiTestGetJson(parameter? :string | null) {

        let params = new HttpParams();
        if (parameter !== null && parameter !== undefined)
            params = params.set('parameter', parameter);

        return this.http.get(`${environment.serverUrl}/api/Test/GetJson`, { params });
    }

    postApiTestPostJson() {
        return this.http.post<ObjectBaseResultInterface>(`${environment.serverUrl}/api/Test/PostJson`, { });
    }

    postApiTestPostJsonRequireAuthorization() {
        return this.http.post<ObjectBaseResultInterface>(`${environment.serverUrl}/api/Test/PostJsonRequireAuthorization`, { });
    }

}
