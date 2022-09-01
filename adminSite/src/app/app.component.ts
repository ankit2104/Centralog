import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'adminSite';
  constructor(private _router:Router){
    
  }
  public logOut(){
    sessionStorage.removeItem("token");
    this._router.navigate(["/Login"]);
  }
  public checkLogIn():boolean{
    if(sessionStorage.getItem("token")!=null){
      return true;
    }
    else{
      return false;
    }
  }
}
