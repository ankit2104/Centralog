import { Project, ProjectReturn } from './../Models/project.model';
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { LoginModel } from '../Models/login.model';
import {
    HttpEvent, HttpInterceptor, HttpHandler,
    HttpRequest, HttpClient, HttpHeaders
} from '@angular/common/http';
import { Register } from "../Models/register.model";
import { LogReturn } from '../Models/log.model';
const endpoint = '/api/';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8', })
};
@Injectable({
    providedIn: 'root'
})
export class RestApiService {
    public token: any;
    constructor(private http: HttpClient) {
    }

    postLogin(Data: LoginModel) {
        return this.http.post<any>(endpoint + "Auth/login", Data, httpOptions);
    }

    register(register: Register) {
        return this.http.post<any>(endpoint + "Auth/register", { Username: register.username, Email: register.email, Password: register.password });
    }

    getProjectList(): Observable<ProjectReturn> {
        return this.http.get<ProjectReturn>(endpoint + "Project/list");
    }

    createProject(project: Project): Observable<any> {
        return this.http.post<any>(endpoint + "Project/create", project);
    }

    updateProject(project: Project): Observable<any> {
        return this.http.post<any>(endpoint + "Project/update", project);
    }

    deleteProject(project: Project): Observable<any> {
        return this.http.post<any>(endpoint + "Project/delete", project);
    }

    getLatestLog(project: Project): Observable<any> {
        return this.http.post<any>(endpoint + "Project/log/latest", project);
    }

    getAllLog(project: Project): Observable<LogReturn> {
        return this.http.post<LogReturn>(endpoint + "Project/log/all", project);
    }

}