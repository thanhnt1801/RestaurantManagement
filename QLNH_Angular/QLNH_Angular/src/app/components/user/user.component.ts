import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Personnel } from 'src/app/_models/personnel';
import { Restaurant } from 'src/app/_models/restaurant';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { ModifyUserRoleComponent } from './modify-user-role/modify-user-role.component';
import { Role } from 'src/app/_models/role';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent {
  loading = true;
  dataSource = new MatTableDataSource<Personnel>();
  selectedRestaurant!: Restaurant | null;
  user: Personnel[] = [];
  displayUser: Personnel[] = [];
  roleNameToEdit: Role | undefined;
  @ViewChild(MatSort) sort = new MatSort();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  columnsToDisplay = [
    'userName',
    'role',
    'description',
    'createdAt',
    'updatedAt',
    'offDuty',
    'deleted',
    'modifyUserRole',
    'edit-btn',
    'delete-btn',
    'restore-btn',
  ];

  constructor(
    public _accountService: AccountService,
    private _toastr: ToastrService,
    private router: Router,
    public dialog: MatDialog
  ) {}

  public ngOnInit() {
    this._accountService.currentRestaurant$.subscribe({
      next: (restaurant) => (this.selectedRestaurant = restaurant),
    });
    if (!this.selectedRestaurant || this.selectedRestaurant.id === 0) {
      this.router.navigateByUrl('/');
    }

    this.loadAllUser();
  }

  loadAllUser() {
    this._accountService.getAllUser().subscribe({
      next: (response) => {
        this.user = response;
        this.displayUser = this.user.filter((user) => {
          return user.restaurant.id === this.selectedRestaurant?.id;
        });
        //hoac
        // this.displayUser = this.user.filter((user) =>
        //   user.restaurant.id === this.selectedRestaurant?.id;
        // );
        //khac nhau o cho {}, neu co {}, minh phai them `return` de gan' dc gia tri
        this.dataSource = new MatTableDataSource(this.displayUser);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.loading = false;
      },
    });
  }

  onDelete(Id: number) {
    this._accountService.DeleteUser(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, true);
        this._toastr.success('Delete successful');
      },
      error: (error) => console.log(error),
    });
  }

  onRestore(Id: number) {
    this._accountService.RestoreUser(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, false);
        this._toastr.success('Restore successful');
      },
      error: (error) => console.log(error),
    });
  }

  updateDeletedValue(Id: number, status: boolean) {
    const user = this.dataSource.data.find((user) => user.id === Id); //dung `restaurant` thay vi (restaurant), () la khia bao danh sach, co the (restaurant, ...)
    if (user) {
      user.deleted = status;
      this.dataSource._updateChangeSubscription();
    }
  }

  async openDialog(userToEdit: Personnel) {
    const dialogRef = this.dialog.open(ModifyUserRoleComponent, {
      width: 'fit-content',
      data: { userToEdit: userToEdit }, //{bien' ma ben kia nhan: gia tri}
    });

    dialogRef.afterClosed().subscribe((result) => {
      if(result)
      {
        // Update the role value in the MatTableDataSource
        const updatedUser = result;
        // console.log ben trong subscribe dc async nen co value, console.log ben ngoai ko co value vi api call back dang trong process
        // this._accountService.getRole(updatedUser?.value.roleId).subscribe({
        //   next: response => {
        //     this.roleNameToEdit = response,
        //     console.log('roleNameToEdit: ',this.roleNameToEdit);
        //   }
        // });
        // console.log('roleNameToEdit11: ',this.roleNameToEdit);

        this._accountService
          .getRole(updatedUser?.value.roleId).subscribe({
            next: response => this.roleNameToEdit = response
          })

        const data = this.dataSource.data;
        const userIndex = data.findIndex((u) => u.id === updatedUser.value.id);
        if (userIndex > -1) {
          data[userIndex].role.name = String(this.roleNameToEdit?.name);
          this.dataSource.data = [...data]; // Trigger data update
          this._toastr.success('Change role successful!');
        }
      }
    }),
      console.log('The dialog was closed!');
  }
}
