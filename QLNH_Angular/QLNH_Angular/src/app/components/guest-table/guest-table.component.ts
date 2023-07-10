import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GuestTable } from 'src/app/_models/guestTable';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-guest-table',
  templateUrl: './guest-table.component.html',
  styleUrls: ['./guest-table.component.scss']
})
export class GuestTableComponent {
  loading = true;
  dataSource = new MatTableDataSource<GuestTable>;
  @ViewChild(MatSort) sort = new MatSort();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  displayGuestTable: GuestTable[] = [];
  currentRestaurant!: Restaurant | null;
  columnsToDisplay = [
    'name',
    'description',
    'location',
    'status',
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

  ngOnInit(){
    this._accountService.currentRestaurant$.subscribe({
      next: response => this.currentRestaurant = response,
      error: error => console.log(error)
    })
    if(!this.currentRestaurant?.id || this.currentRestaurant.id < 1)
    {
      this.router.navigateByUrl('/');
    }
    this.loadGuestTablees();
  }

  loadGuestTablees(){
    this._accountService.getAllGuestTable().subscribe({
      next: response => {
        this.displayGuestTable = response.filter(
          (guesttable) => guesttable.restaurantId === this.currentRestaurant?.id);
        console.log('guest table: ', this.displayGuestTable);
        this.dataSource = new MatTableDataSource(this.displayGuestTable);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.loading = false;
      }
    })
  }

  onDelete(Id : number){
    this._accountService.DeleteGuestTable(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, true);
        this.toastr.success('Delete successful');
      },
      error: error => console.log(error)
    })
  }

  onRestore(Id : number){
    this._accountService.RestoreGuestTable(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, false);
        this.toastr.success('Restore successful');
      },
      error: error => console.log(error)
    })
  }

  updateDeletedValue(Id : number, status: boolean){
    const guestTableToDelete = this.dataSource.data.find(guestTable => guestTable.id === Id) //dung `restaurant` thay vi (restaurant), () la khia bao danh sach, co the (restaurant, ...)
    if(guestTableToDelete){
      guestTableToDelete.deleted = status;
      this.dataSource._updateChangeSubscription();
    }
  }

}
