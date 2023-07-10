import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Personnel } from 'src/app/_models/personnel';
import { Role } from 'src/app/_models/role';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent {
  editForm: FormGroup = new FormGroup({});
  user: Personnel | undefined;
  id : number = 0;
  displayRole : Role[] = [];
  constructor(private _accountService: AccountService,
      private toastr: ToastrService,
      private actRoute: ActivatedRoute,
      private fb: FormBuilder,
      private router: Router){}

  ngOnInit(){
    this.id = Number(this.actRoute.snapshot.paramMap.get('id'));
    this.getAllRole();
    this.getUserToEdit();
    this.initializeForm();
  }

  getUserToEdit(){
    this._accountService.getUser(this.id).subscribe({
      next: response => {
        this.editForm.setValue({
          id: response.id,
          userName: response.userName,
          description: response.description,
          offDuty: response.offDuty
        });
        this.user = response;
      },
      error: error => console.log(error)
    })
  }

  getAllRole(){
    this._accountService.getAllRole().subscribe({
      next: response => {
        this.displayRole = response
      }
    });
  }

  initializeForm(){
    this.editForm = this.fb.group({
      userName: ['', Validators.required],
      description: ['', Validators.required],
      offDuty: ['', Validators.required],
      id : []
    });
  }

  onEditUser(){
    if(this.editForm.valid){
      this._accountService.EditUser(this.editForm?.value).subscribe({
        next: () => {
          this.toastr.success('Edit restaurant successful'),
          this.router.navigateByUrl('/user')
        },
        error: error => console.log(error)
      })
    }
    
  }
}
