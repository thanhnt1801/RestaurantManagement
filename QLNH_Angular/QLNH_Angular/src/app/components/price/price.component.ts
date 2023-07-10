import { Component, Input, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';
import { Price } from 'src/app/_models/price';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-price',
  templateUrl: './price.component.html',
  styleUrls: ['./price.component.scss']
})
export class PriceComponent {
  @Input('selectedItem') selectedItem : Item | undefined;
  loading = true;
  dataSource = new MatTableDataSource<Price>;
  @ViewChild(MatSort) sort = new MatSort();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  displayPrice: Price[] = [];
  currentRestaurant!: Restaurant | null;
  columnsToDisplay = [
    'unit',
    'size',
    'description',
    'salePrice',
    'created',
    'updated',
    'deleted',
    'edit-btn',
    'delete-btn',
    'restore-btn',
  ];
  constructor(private _accountService : AccountService,
      private toastr:ToastrService,
      private router: Router){}

  public ngOnInit() : void
  {
    this._accountService.currentRestaurant$.subscribe({
      next: response => this.currentRestaurant = response,
      error: error => console.log(error)
    })
    if(!this.currentRestaurant?.id || this.currentRestaurant.id < 1)
    {
      this.router.navigateByUrl('/');
    }
    this.loadPricees();
  }

  loadPricees(){
    this._accountService.getAllPrice().subscribe({
      next: response => {
        this.displayPrice = response.filter(
          (price) => price.restaurantId === this.currentRestaurant?.id
                  && price.itemId === this.selectedItem?.id)
        this.dataSource = new MatTableDataSource(this.displayPrice);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.loading = false;
      }
    })
  }

  onDelete(Id : number){
    this._accountService.DeletePrice(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, true);
        this.toastr.success('Delete successful');
      },
      error: error => console.log(error)
    })
  }

  onRestore(Id : number){
    this._accountService.RestorePrice(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, false);
        this.toastr.success('Restore successful');
      },
      error: error => console.log(error)
    })
  }

  updateDeletedValue(Id : number, status: boolean){
    const priceToDelete = this.dataSource.data.find(price => price.id === Id) //dung `restaurant` thay vi (restaurant), () la khia bao danh sach, co the (restaurant, ...)
    if(priceToDelete){
      priceToDelete.deleted = status;
      this.dataSource._updateChangeSubscription();
    }
  }
}
