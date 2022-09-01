import { ProjectComponent } from './Pages/ProjectList/Project.component';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule } from "@angular/common/http";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { RestApiService } from './Services/rest.service';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { LoginPage } from './Pages/LoginPage/LoginPage.component';
import { RegisterComponent } from './Pages/Registration/Register.component';
import { CreateProject } from './Pages/ProjectList/CreateProject/CreateProject.component';
import { EditProject } from './Pages/ProjectList/EditProject/EditProject.component';
import { ApiInterceptor } from './Interceptor/api.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { LocalStorageService } from './Services/localStorage.service';
import { AllLogComponent } from './Pages/Logs/AllLog/AllLog.component';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { LiveLogComponent } from './Pages/Logs/Log/LiveLog.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginPage,
    RegisterComponent,
    ProjectComponent,
    CreateProject,
    EditProject,
    AllLogComponent,
    LiveLogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatDialogModule
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: [HttpClient, RestApiService,LocalStorageService,
    { provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
