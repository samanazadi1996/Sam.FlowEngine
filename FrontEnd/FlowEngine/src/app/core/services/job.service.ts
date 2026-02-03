// Auto-generated service for tag: Job
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { IdTitleDtoListBaseResultInterface } from './interfaces/id-title-dto-list-base-result-interface';
import { ProjectJobDtoBaseResultInterface } from './interfaces/project-job-dto-base-result-interface';
import { UpdatePositionJobCommandInterface } from './interfaces/update-position-job-command-interface';
import { BaseResultInterface } from './interfaces/base-result-interface';
import { UpdateJobCommandInterface } from './interfaces/update-job-command-interface';

@Injectable({ providedIn: 'root' })
export class JobService {
    constructor(private http: HttpClient) { }


    getApiJobGetAllJobsByProjectId(projectId? :number | null) {

        let params = new HttpParams();
        if (projectId !== null && projectId !== undefined)
            params = params.set('ProjectId', projectId);

        return this.http.get<IdTitleDtoListBaseResultInterface>(`${environment.serverUrl}/api/Job/GetAllJobsByProjectId`, { params });
    }

    getApiJobGetJobById(id? :number | null) {

        let params = new HttpParams();
        if (id !== null && id !== undefined)
            params = params.set('Id', id);

        return this.http.get<ProjectJobDtoBaseResultInterface>(`${environment.serverUrl}/api/Job/GetJobById`, { params });
    }

    putApiJobUpdatePositionJob(body : UpdatePositionJobCommandInterface) {
        return this.http.put<BaseResultInterface>(`${environment.serverUrl}/api/Job/UpdatePositionJob`, body);
    }

    putApiJobUpdateJob(body : UpdateJobCommandInterface) {
        return this.http.put<BaseResultInterface>(`${environment.serverUrl}/api/Job/UpdateJob`, body);
    }

}
