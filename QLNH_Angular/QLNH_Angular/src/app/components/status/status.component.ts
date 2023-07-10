import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Restaurant } from 'src/app/_models/restaurant';
import { Status } from 'src/app/_models/status';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.scss']
})
export class StatusComponent {
  loading = true;
  dataSource = new MatTableDataSource<Status>;
  @ViewChild(MatSort) sort = new MatSort();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  displayStatus: Status[] = [];
  currentRestaurant!: Restaurant | null;
  columnsToDisplay = [
    'name',
    'description',
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
    this.loadStatuses();
  }

  loadStatuses(){
    this._accountService.getAllStatus().subscribe({
      next: response => {
        this.displayStatus = response.filter(
          (status) => status.restaurantId === this.currentRestaurant?.id)
        this.dataSource = new MatTableDataSource(this.displayStatus);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.loading = false;
      }
    })
  }

  onDelete(Id : number){
    this._accountService.DeleteStatus(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, true);
        this.toastr.success('Delete successful');
      },
      error: error => console.log(error)
    })
  }

  onRestore(Id : number){
    this._accountService.RestoreStatus(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, false);
        this.toastr.success('Restore successful');
      },
      error: error => console.log(error)
    })
  }

  updateDeletedValue(Id : number, status: boolean){
    const statusToDelete = this.dataSource.data.find(status => status.id === Id) //dung `restaurant` thay vi (restaurant), () la khia bao danh sach, co the (restaurant, ...)
    if(statusToDelete){
      statusToDelete.deleted = status;
      this.dataSource._updateChangeSubscription();
    }
  }
}
