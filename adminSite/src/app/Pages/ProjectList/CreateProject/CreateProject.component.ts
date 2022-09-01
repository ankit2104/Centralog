import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { Project } from 'src/app/Models/project.model';
import { RestApiService } from 'src/app/Services/rest.service';

@Component({
    selector: 'app-Logs',
    templateUrl: './CreateProject.component.html'
})
export class CreateProject implements OnInit {
    title = 'Project';
    public project: Project;
    constructor(private _router:Router,private _api:RestApiService) {
    }
    ngOnInit(): void {
        let token = sessionStorage.getItem("token");
        if (token == null) {
            this._router.navigate(['/Login']);
        }
        this.project=new Project();
        this.project.id="";
        this.project.user_id="";
    }
    goBack(){
        this._router.navigate(['/Project']);
    }
    submit(){
        this._api.createProject(this.project).subscribe((response)=>{
            if(response.status=="success"){
                alert("Project Created!");
                this.goBack();
            }
            else{
                alert("Project creation Error!");
            }
        })
    }
}