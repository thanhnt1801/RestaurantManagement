import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-edit-role',
  templateUrl: './edit-role.component.html',
  styleUrls: ['./edit-role.component.scss']
})
export class EditRoleComponent {
  editForm: FormGroup = new FormGroup({});
  id : number = 0;
  constructor(private _accountService : AccountService,
      private toastr : ToastrService,
      private fb: FormBuilder,
      private actRoute: ActivatedRoute,
      private router: Router){}

  ngOnInit(){
    this.id = +this.actRoute.snapshot.paramMap.get('id')!;
    this.getRoleToEdit();
    this.initializeForm();
  }

  getRoleToEdit(){
    this._accountService.getRole(this.id).subscribe({
      next: response => {
        this.editForm.setValue({
          name: response.name,
          description: response.description,
          id: response.id
        });
      },
      error: error => console.log(error)
    })
  }

  initializeForm(){
    this.editForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      id: []
    });
  }

  onEditRole(){
    if(this.editForm?.valid){
      this._accountService.EditRole(this.editForm?.value).subscribe({
        next: () => {
          this.toastr.success('Edit successful'),
          this.router.navigateByUrl('/role')
        },
        error: error => console.log(error)
      })
    }
  }
}
