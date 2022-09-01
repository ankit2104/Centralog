import { Router } from '@angular/router';
import { Project } from 'src/app/Models/project.model';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { LocalStorageService } from 'src/app/Services/localStorage.service';
@Component({
    selector: 'app-Logs',
    templateUrl: './Project.component.html'
})
export class ProjectComponent implements OnInit {
    title = 'Project';
    public projects: Project;
    public deleteProject: Project;
    dataSource: any;
    displayedColumns = ['id', 'name', 'created', 'logCount', "action"];
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild('searchInput', { static: true }) searchInput: ElementRef;
    loading = true;
    isPreloadTextViewed = true;
    projectList: Project[]=[];
    constructor(
        private service: RestApiService,
        public dialog: MatDialog,
        private _router: Router,
        private _localStorage:LocalStorageService
    ) { }
    ngOnInit(): void {
        let token = sessionStorage.getItem("token");
        if (token == null) {
            this._router.navigate(['/Login']);
        }
        this.getAllProjects();
    }

    getAllProjects() {

        this.service.getProjectList().subscribe(
            Projects => {
                console.log(Projects)
                this.projectList = Projects.projectDetails;
                this.dataSource = new MatTableDataSource(this.projectList);
                console.log(this.dataSource);
                this.dataSource.paginator = this.paginator;
                this.isPreloadTextViewed = false;
                this.loading = false;
            },
            err => {

                this.isPreloadTextViewed = false;
                this.loading = false;
                console.log(err);
            }
        );
    }
    // editCustomer(index) {
    //     const _saveMessage = `Contact Updated successfully.`;
    //     const _messageType = MessageType.Update;
    //     this.QuoteContract = this.AllQuoteContract[index];
    //     const dialogRef = this.dialog.open(ContactDialogComponent, { data: this.QuoteContract, width: '300px' });
    //     dialogRef.afterClosed().subscribe(res => {
    //         if (!res) {
    //             return;
    //         }

    //         this.AllQuoteContract[index] = res.data;
    //         this.layoutUtilsService.showActionNotification(_saveMessage, _messageType, 10000, true, true);
    //         this.allQuoteContractlength = this.AllQuoteContract.length > 0 ? true : false;
    //     });


    // }

    Delete(id: string): void {
        this.deleteProject=new Project();
        this.deleteProject.id= id;
        this.deleteProject.name="";
        this.deleteProject.user_id="";
        this.service.deleteProject(this.deleteProject).subscribe
            (
                response => {
                    if (response.status == "success") {
                        this.getAllProjects();
                        alert("Project Deleted");
                    }
                    else {
                        alert("Something Went Wrong");
                    }
                })
    }
    public create(){
        this._router.navigate(['/Create'])
    }
    public ViewLogs(proj:Project){
        this._localStorage.setProject(proj);
        this._router.navigate(["/Logs"]);
    }
    public logOut(){
        sessionStorage.removeItem("token");
        this._router.navigate(["/Login"]);
    }

}