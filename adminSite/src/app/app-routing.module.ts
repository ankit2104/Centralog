import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPage } from './Pages/LoginPage/LoginPage.component';
import { AllLogComponent } from './Pages/Logs/AllLog/AllLog.component';
import { LiveLogComponent } from './Pages/Logs/Log/LiveLog.component';
import { CreateProject } from './Pages/ProjectList/CreateProject/CreateProject.component';
import { ProjectComponent } from './Pages/ProjectList/Project.component';
import { RegisterComponent } from './Pages/Registration/Register.component';

const routes: Routes = [
  { path: 'Login', component: LoginPage },
  { path: '', redirectTo: 'Login', pathMatch: "full" },
  { path: 'Register', component: RegisterComponent },
  { path: 'Project', component: ProjectComponent },
  {path:'Create',component:CreateProject},
  {path:'Logs',component:AllLogComponent},
  {path:'LiveLogs',component:LiveLogComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
