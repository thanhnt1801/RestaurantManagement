import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { LoginComponent } from '../login/login.component';
import { Restaurant } from 'src/app/_models/restaurant';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {
  model : any = {};
  currentRestaurant! : Restaurant | null;

  constructor(private _services:AccountService
    , public _accountSerive: AccountService
    , private router: Router
    , private toastr: ToastrService
    , public dialog: MatDialog){}

  public ngOnInit() : void
  {
    this._accountSerive.currentRestaurant$.subscribe({
      next: selectedRestaurant => {
        this.currentRestaurant = selectedRestaurant;
        console.log('current: ',this.currentRestaurant);
      },
      error: error => console.log(error)
    })
  }


  public login(){
    this._services.login(this.model).subscribe({
      next: _ => {
        this.router.navigateByUrl("/restaurant");
      }
    });
  }

  public logout(){
    this.router.navigateByUrl("/");
    this._accountSerive.logout();
  }

  openDialog() : void {
    const dialogRef = this.dialog.open(LoginComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed!');
    })
  }

}
