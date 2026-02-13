// Auto-generated service for tag: Doc
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { Int32StringDictionaryBaseResultInterface } from './interfaces/int32-string-dictionary-base-result-interface';
import { StringStringStringDictionaryDictionaryBaseResultInterface } from './interfaces/string-string-string-dictionary-dictionary-base-result-interface';
import { StringStringDictionaryBaseResultInterface } from './interfaces/string-string-dictionary-base-result-interface';

@Injectable({ providedIn: 'root' })
export class DocService {
    constructor(private http: HttpClient) { }


    getApiDocGetErrorCodes() {

        let params = new HttpParams();
        
        return this.http.get<Int32StringDictionaryBaseResultInterface>(`${environment.serverUrl}/api/Doc/GetErrorCodes`, { params });
    }

    getApiDocGetDomainEnums() {

        let params = new HttpParams();
        
        return this.http.get<StringStringStringDictionaryDictionaryBaseResultInterface>(`${environment.serverUrl}/api/Doc/GetDomainEnums`, { params });
    }

    getApiDocValuePlaceholderProcessorReplacements() {

        let params = new HttpParams();
        
        return this.http.get<StringStringDictionaryBaseResultInterface>(`${environment.serverUrl}/api/Doc/ValuePlaceholderProcessorReplacements`, { params });
    }

}
