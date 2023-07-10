import { Component, Input, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Restaurant } from 'src/app/_models/restaurant';
import { Size } from 'src/app/_models/size';
import { Unit } from 'src/app/_models/unit';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-size',
  templateUrl: './size.component.html',
  styleUrls: ['./size.component.scss']
})
export class SizeComponent {
  @Input('selectedUnit') selectedUnit: Unit | undefined;
  loading = true;
  dataSource = new MatTableDataSource<Size>;
  @ViewChild(MatSort) sort = new MatSort();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  displaySize: Size[] = [];
  currentRestaurant!: Restaurant | null;
  columnsToDisplay = [
    'name',
    'unit',
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
    this.loadSizees();
  }

  loadSizees(){
    this._accountService.getAllSize().subscribe({
      next: response => {
        this.displaySize = response.filter(
          (size) => {
            return size.restaurantId === this.currentRestaurant?.id
              && size.unit.id === this.selectedUnit?.id
          })
        this.dataSource = new MatTableDataSource(this.displaySize);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.loading = false;
      }
    })
  }

  onDelete(Id : number){
    this._accountService.DeleteSize(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, true);
        this.toastr.success('Delete successful');
      },
      error: error => console.log(error)
    })
  }

  onRestore(Id : number){
    this._accountService.RestoreSize(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, false);
        this.toastr.success('Restore successful');
      },
      error: error => console.log(error)
    })
  }

  updateDeletedValue(Id : number, status: boolean){
    const sizeToDelete = this.dataSource.data.find(size => size.id === Id) //dung `restaurant` thay vi (restaurant), () la khia bao danh sach, co the (restaurant, ...)
    if(sizeToDelete){
      sizeToDelete.deleted = status;
      this.dataSource._updateChangeSubscription();
    }
  }
}
