import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  registerMode = false;
  loading = true;
  users: any;
  selectedRestaurant: Restaurant | undefined;
  restaurants: Restaurant[] = [];
  constructor(private http: HttpClient, public accountService : AccountService) {
  }

  ngOnInit(){
    this.loadAllRestaurant();
  }

  onRestaurantChange(event: any){
    const restaurant: Restaurant = event;
    this.accountService.currentRestaurantSource.next(restaurant);
  }

  loadAllRestaurant(){
    this.accountService.getAllRestaurant().subscribe({
      next: response => {
        this.restaurants = response.filter((restaurant) => {
          return restaurant.deleted === false
        });
        // console.log('load restaurant to select: ', this.restaurants);
      }
    })
  }

  registerToggle()
  {
    this.registerMode = !this.registerMode;
  }

  CancelRegisterMode(event : boolean)
  {
    this.registerMode = event;
  }
}
