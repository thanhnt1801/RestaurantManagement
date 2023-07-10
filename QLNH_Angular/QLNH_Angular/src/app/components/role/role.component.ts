import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { Role } from 'src/app/_models/role';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent {
  loading = true;
  roleFromJwt : boolean = false;
  dataSource = new MatTableDataSource<Role>;
  @ViewChild(MatSort) sort = new MatSort();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
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
  constructor(public _accountService : AccountService,
      private toastr:ToastrService){}

  public ngOnInit() : void
  {
    this.loadRoles();
    this.roleFromJwt = this._accountService.getRoleFromJwt();
  }

  loadRoles(){
    this._accountService.getAllRole().subscribe({
      next: response => {
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.loading = false;
      }
    })
  }

  onDelete(Id : number){
    this._accountService.DeleteRole(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, true);
        this.toastr.success('Delete successful');
      },
      error: error => console.log(error)
    })
  }

  onRestore(Id : number){
    this._accountService.RestoreRole(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, false);
        this.toastr.success('Restore successful');
      },
      error: error => console.log(error)
    })
  }

  updateDeletedValue(Id : number, status: boolean){
    const role = this.dataSource.data.find(role => role.id === Id) //dung `restaurant` thay vi (restaurant), () la khia bao danh sach, co the (restaurant, ...)
    if(role){
      role.deleted = status;
      this.dataSource._updateChangeSubscription();
    }
  }
}
