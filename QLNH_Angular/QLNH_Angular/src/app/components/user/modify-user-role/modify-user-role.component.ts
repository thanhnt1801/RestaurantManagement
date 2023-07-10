import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Personnel } from 'src/app/_models/personnel';
import { Role } from 'src/app/_models/role';
import { AccountService } from 'src/app/_services/account.service';

export interface DialogData{
  userToEdit: Personnel;
}

@Component({
  selector: 'app-modify-user-role',
  templateUrl: './modify-user-role.component.html',
  styleUrls: ['./modify-user-role.component.scss']
})
export class ModifyUserRoleComponent {
  modifyRoleForm: FormGroup = new FormGroup({});
  displayRole: Role[] = []

  constructor(public dialogRef: MatDialogRef<ModifyUserRoleComponent>,
      private _accountService: AccountService,
      private router:Router,
      private toastr: ToastrService,
      private fb: FormBuilder,
      @Inject(MAT_DIALOG_DATA) public data: DialogData,){}

  ngOnInit(){
    this.getAllRole();
    this.getUserToEdit();
    this.initializaForm();
  }

  getAllRole(){
    this._accountService.getAllRole().subscribe({
      next : response => {
        this.displayRole = response;
      },
      error: error => console.log('AllRoleError: ', error)
    })
  }

  getUserToEdit(){
    this._accountService.getUser(this.data.userToEdit.id).subscribe({
      next: response => {
        this.modifyRoleForm.setValue({
          id: response.id,
          roleId: response.roleId
        });
      },
      error: error => console.log(error)
    })
  }

  initializaForm(){
    this.modifyRoleForm = this.fb.group({
      id: ['', Validators.required,],
      roleId: ['', Validators.required]
    });
  }

  onModify(){
    this._accountService.AddUserToRole(this.modifyRoleForm?.value).subscribe({
      next: response => this.router.navigateByUrl("/user"),
    })
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
