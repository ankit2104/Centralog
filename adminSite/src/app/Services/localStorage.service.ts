import { Project, ProjectReturn } from './../Models/project.model';
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { LoginModel } from '../Models/login.model';
@Injectable({
    providedIn: 'root'
})
export class LocalStorageService {
    private project:Project
    public LocalStorageService(){

    }
    public getProject():Project{
        return this.project;
    }
    public setProject(proj:Project){
        this.project=proj;
    }
}