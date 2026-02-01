// Auto-generated service for tag: Job
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { ProjectJobListBaseResultInterface } from './interfaces/project-job-list-base-result-interface';
import { UpdatePositionJobCommandInterface } from './interfaces/update-position-job-command-interface';
import { BaseResultInterface } from './interfaces/base-result-interface';

@Injectable({ providedIn: 'root' })
export class JobService {
    constructor(private http: HttpClient) { }


    getApiJobGetAllJobs() {

        let params = new HttpParams();
        
        return this.http.get<ProjectJobListBaseResultInterface>(`${environment.serverUrl}/api/Job/GetAllJobs`, { params });
    }

    putApiJobUpdatePositionJob(body : UpdatePositionJobCommandInterface) {
        return this.http.put<BaseResultInterface>(`${environment.serverUrl}/api/Job/UpdatePositionJob`, body);
    }

}
