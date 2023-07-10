import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.scss']
})
export class ItemComponent {
  public responsiveOptions = [
    {
      breakpoint: '1600px',
      numVisible: 3,
      numScroll: 3,
    },
    {
      breakpoint: '300px',
      numVisible: 2,
      numScroll: 2,
    },
    {
      breakpoint: '200px',
      numVisible: 1,
      numScroll: 1,
    },
  ];
  items: Item[] = [];
  itemToEdit?: Item;
  displayItems: Item[] = [];
  selectedRestaurant!: Restaurant | null;
  loading: boolean = true;
  
  constructor(
    private _accountService: AccountService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this._accountService.currentRestaurant$.subscribe((restaurant) => {
      this.selectedRestaurant = restaurant;
    });
    if (!this.selectedRestaurant || this.selectedRestaurant.id === 0) {
      this.router.navigate(['/']);
    }
    this.loadItem();
  }

  public loadItem(): void {
    this.loading = true;
    this._accountService.getAllItem().subscribe((data) => {
      this.items = data;
      this.displayItems = this.items.filter(
        (item) => item.restaurantId === this.selectedRestaurant?.id
      );

      console.log('Items:', this.displayItems);
      this.loading = false;
    });
  }

  onDelete(Id : number){
    this._accountService.DeleteItem(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, true);
        this.toastr.success('Delete successful');
      },
      error: error => console.log(error)
    })
  }

  onRestore(Id : number){
    this._accountService.RestoreItem(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, false);
        this.toastr.success('Restore successful');
      },
      error: error => console.log(error)
    })
  }

  updateDeletedValue(Id : number, status: boolean){
    const itemToDelete = this.displayItems.find(item => item.id === Id) //dung `restaurant` thay vi (restaurant), () la khia bao danh sach, co the (restaurant, ...)
    if(itemToDelete){
      itemToDelete.deleted = status;
    }
  }
}
