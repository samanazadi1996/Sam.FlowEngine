// Auto-generated service for tag: Project
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import { GetUserProjectsResponseListBaseResultInterface } from './interfaces/get-user-projects-response-list-base-result-interface';
import { ProjectDtoBaseResultInterface } from './interfaces/project-dto-base-result-interface';
import { StartProjectCommandInterface } from './interfaces/start-project-command-interface';
import { BaseResultInterface } from './interfaces/base-result-interface';
import { StopProjectCommandInterface } from './interfaces/stop-project-command-interface';
import { CreateProjectCommandInterface } from './interfaces/create-project-command-interface';

@Injectable({ providedIn: 'root' })
export class ProjectService {
    constructor(private http: HttpClient) { }


    getApiProjectGetUserProjects() {

        let params = new HttpParams();
        
        return this.http.get<GetUserProjectsResponseListBaseResultInterface>(`${environment.serverUrl}/api/Project/GetUserProjects`, { params });
    }

    getApiProjectGetUserProjectByName(projectName? :string | null) {

        let params = new HttpParams();
        if (projectName !== null && projectName !== undefined)
            params = params.set('ProjectName', projectName);

        return this.http.get<ProjectDtoBaseResultInterface>(`${environment.serverUrl}/api/Project/GetUserProjectByName`, { params });
    }

    postApiProjectStartProject(body : StartProjectCommandInterface) {
        return this.http.post<BaseResultInterface>(`${environment.serverUrl}/api/Project/StartProject`, body);
    }

    postApiProjectStopProject(body : StopProjectCommandInterface) {
        return this.http.post<BaseResultInterface>(`${environment.serverUrl}/api/Project/StopProject`, body);
    }

    postApiProjectCreateProject(body : CreateProjectCommandInterface) {
        return this.http.post<BaseResultInterface>(`${environment.serverUrl}/api/Project/CreateProject`, body);
    }

    postApiProjectCteateTemplate(templateName? :string | null) {
        return this.http.post<BaseResultInterface>(`${environment.serverUrl}/api/Project/CteateTemplate`, { });
    }

}
