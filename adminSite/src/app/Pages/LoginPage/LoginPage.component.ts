import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { LoginModel } from 'src/app/Models/login.model';

@Component({
    selector: 'app-Login',
    templateUrl: './LoginPage.component.html'
})
export class LoginPage implements OnInit {
    title = 'Log In';
    public login: LoginModel;
    public Username: string;
    public Password: string;
    public error: string;
    constructor(private _api: RestApiService, private _router: Router) {
        this.login = new LoginModel();
        let token = sessionStorage.getItem("token");
        if (token != null) {
            //this._router.navigate(['/Dashboard']);
            console.log("Login Success");
        }
        this.error = "";
    }
    ngOnInit(): void {
        let token = sessionStorage.getItem("token");
        if (token != null) {
            this._router.navigate(['/Project']);
        }

    }
    public LoginButton() {
        if (this.login.Password == null || this.login.Password == "") {
            this.error = "Password and Username must not be empty";
        }
        else if (this.login.Username == null || this.login.Username == "") {
            this.error = "Password and Username must not be empty";
        }
        else {
            debugger;
            this._api.postLogin(this.login).subscribe((data) => {
                debugger;
                if (!data.token) {
                    sessionStorage.setItem("token", "");
                    this.error = "Invalid Username or Password";
                    console.log("fail");
                }
                else {
                    sessionStorage.setItem("token", data.token);
                    console.log(data.token);
                    this._router.navigate(['/Project']);
                    console.log("Login Successful");
                }
            });
        }
    }
}