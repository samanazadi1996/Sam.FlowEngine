// Auto-generated service for tag: Job
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { ProjectJobDtoListBaseResultInterface } from './interfaces/project-job-dto-list-base-result-interface';
import { IdTitleDtoListBaseResultInterface } from './interfaces/id-title-dto-list-base-result-interface';
import { ProjectJobDtoBaseResultInterface } from './interfaces/project-job-dto-base-result-interface';
import { CreateJobCommandInterface } from './interfaces/create-job-command-interface';
import { BaseResultInterface } from './interfaces/base-result-interface';
import { UpdatePositionJobCommandInterface } from './interfaces/update-position-job-command-interface';
import { UpdateJobCommandInterface } from './interfaces/update-job-command-interface';

@Injectable({ providedIn: 'root' })
export class JobService {
    constructor(private http: HttpClient) { }


    getApiJobGetAllJobs() {

        let params = new HttpParams();
        
        return this.http.get<ProjectJobDtoListBaseResultInterface>(`${environment.serverUrl}/api/Job/GetAllJobs`, { params });
    }

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

    postApiJobCreateJob(body : CreateJobCommandInterface) {
        return this.http.post<BaseResultInterface>(`${environment.serverUrl}/api/Job/CreateJob`, body);
    }

    putApiJobUpdatePositionJob(body : UpdatePositionJobCommandInterface) {
        return this.http.put<BaseResultInterface>(`${environment.serverUrl}/api/Job/UpdatePositionJob`, body);
    }

    putApiJobUpdateJob(body : UpdateJobCommandInterface) {
        return this.http.put<BaseResultInterface>(`${environment.serverUrl}/api/Job/UpdateJob`, body);
    }

    deleteApiJobDeleteJob(id? :number | null) {

        let params = new HttpParams();
        if (id !== null && id !== undefined)
            params = params.set('Id', id);

        return this.http.delete<BaseResultInterface>(`${environment.serverUrl}/api/Job/DeleteJob`, { params });
    }

}
