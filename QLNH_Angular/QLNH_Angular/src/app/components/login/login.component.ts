import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginModel: any = {
    userName: '',
    password: ''
  };

  constructor(private _accountSerive: AccountService,
    private router: Router,
    public dialogRef: MatDialogRef<LoginComponent>){}

  onCreate(loginModel : any){
    this._accountSerive.login(loginModel).subscribe({
      next: response => this.router.navigateByUrl("/"),
    })
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
