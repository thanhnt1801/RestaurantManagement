import { Component, Input, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.scss']
})
export class RestaurantComponent {
  loading = true;
  dataSource = new MatTableDataSource<Restaurant>;
  @ViewChild(MatSort) sort = new MatSort();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  columnsToDisplay = [
    'name',
    'phone',
    'description',
    'address',
    'created',
    'updated',
    'deleted',
    'edit-btn',
    'delete-btn',
    'restore-btn',
  ];
  constructor(public _accountService : AccountService,
      private toastr:ToastrService){}

  public ngOnInit() : void
  {
    this.loadRestaurants();
  }

  // ngOnChanges(){ this.dataSource.sort = this.sort;  }

  loadRestaurants()
  {
    this._accountService.getAllRestaurant().subscribe({
      next: response => 
      {
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.loading = false;
      }
    });
  }

  onDelete(restaurantId : number){
    this._accountService.DeleteRestaurant(restaurantId).subscribe({
      next: () => {
        this.updateDeletedValue(restaurantId, true);
        this.toastr.success('Delete restaurant successful');
      },
      error: error => console.log(error)
    })
  }

  onRestore(restaurantId : number){
    this._accountService.RestoreRestaurant(restaurantId).subscribe({
      next: () => {
        this.updateDeletedValue(restaurantId, false);
        this.toastr.success('Restore restaurant successful');
      },
      error: error => console.log(error)
    })
  }

  updateDeletedValue(Id : number, status: boolean){
    const restaurant = this.dataSource.data.find(restaurant => restaurant.id === Id) //dung `restaurant` thay vi (restaurant), () la khia bao danh sach, co the (restaurant, ...)
    if(restaurant){
      restaurant.deleted = status;
      this.dataSource._updateChangeSubscription();
    }
  }
}
