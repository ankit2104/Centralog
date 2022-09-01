import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { Register } from '../../Models/register.model';

@Component({
    selector: 'app-Register',
    templateUrl: './Register.component.html'
})
export class RegisterComponent implements OnInit {
    public register: Register;
    constructor(private _api: RestApiService, private _router: Router) {
        this.register = new Register();
    }
    ngOnInit(): void {

    }
    Register() {
        debugger;
        this._api.register(this.register).subscribe((data) => {
            if (data.status == "Success") {
                alert("Register Successful");
                // sessionStorage.setItem("token", data.token);
                // console.log(data.token);
                this._router.navigate(['/Login']);
            }
            else{
                sessionStorage.setItem("token", '');
                console.log("fail");
                alert("Registering Failed Please try again");
            }
        });
    }
}