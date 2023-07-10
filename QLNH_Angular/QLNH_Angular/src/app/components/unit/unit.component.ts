import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Restaurant } from 'src/app/_models/restaurant';
import { Unit } from 'src/app/_models/unit';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-unit',
  templateUrl: './unit.component.html',
  styleUrls: ['./unit.component.scss']
})
export class UnitComponent {
  loading = true;
  dataSource = new MatTableDataSource<Unit>;
  @ViewChild(MatSort) sort = new MatSort();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  displayUnit: Unit[] = [];
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
    this.loadUnits();
  }

  loadUnits(){
    this._accountService.getAllUnit().subscribe({
      next: response => {
        this.displayUnit = response.filter(
          (unit) => unit.restaurantId === this.currentRestaurant?.id)
        this.dataSource = new MatTableDataSource(this.displayUnit);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.loading = false;
      }
    })
  }

  onDelete(Id : number){
    this._accountService.DeleteUnit(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, true);
        this.toastr.success('Delete successful');
      },
      error: error => console.log(error)
    })
  }

  onRestore(Id : number){
    this._accountService.RestoreUnit(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, false);
        this.toastr.success('Restore successful');
      },
      error: error => console.log(error)
    })
  }

  updateDeletedValue(Id : number, status: boolean){
    const unitToDelete = this.displayUnit.find(unit => unit.id === Id) //dung `restaurant` thay vi (restaurant), () la khia bao danh sach, co the (restaurant, ...)
    if(unitToDelete){
      unitToDelete.deleted = status;
    }
  }

  calculateCustomerTotal(name: any) {
    let total = 0;

    if (this.displayUnit) {
      for (let unit of this.displayUnit) {
        if (unit.name === name) {
          total++;
        }
      }
    }
    return total;
  }
}
