import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';
import { Restaurant } from './_models/restaurant';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'QLNH_Angular';
  users: any;

  constructor(private http:HttpClient, private _accountService: AccountService){}

  ngOnInit() : void {
    this.setCurrentUser();
    this._accountService.getRoleFromJwt();
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString) return;
    const user:User = JSON.parse(userString);
    this._accountService.SetCurrentUser(user);
  }
}
