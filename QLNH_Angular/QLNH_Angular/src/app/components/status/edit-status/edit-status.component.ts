import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-edit-status',
  templateUrl: './edit-status.component.html',
  styleUrls: ['./edit-status.component.scss']
})
export class EditStatusComponent {
  editForm: FormGroup = new FormGroup({});
  id : number = 0;
  constructor(private _accountService : AccountService,
      private toastr : ToastrService,
      private fb: FormBuilder,
      private actRoute: ActivatedRoute,
      private router: Router){}

  ngOnInit(){
    this.id = +this.actRoute.snapshot.paramMap.get('id')!;
    this.getStatusToEdit();
    this.initializeForm();
  }

  getStatusToEdit(){
    this._accountService.getStatus(this.id).subscribe({
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

  onEditStatus(){
    if(this.editForm?.valid){
      this._accountService.EditStatus(this.editForm?.value).subscribe({
        next: () => {
          this.toastr.success('Edit successful'),
          this.router.navigateByUrl('/status')
        },
        error: error => console.log(error)
      })
    }
  }
}
