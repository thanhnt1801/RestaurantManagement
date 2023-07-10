import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.scss']
})
export class EditItemComponent {
  editForm: FormGroup = new FormGroup({});
  id : number = 0;
  constructor(private _accountService : AccountService,
      private toastr : ToastrService,
      private fb: FormBuilder,
      private actRoute: ActivatedRoute,
      private router: Router){}

  ngOnInit(){
    this.id = +this.actRoute.snapshot.paramMap.get('id')!;
    this.getItemToEdit();
    this.initializeForm();
  }

  getItemToEdit(){
    this._accountService.getItem(this.id).subscribe({
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

  onEditItem(){
    if(this.editForm?.valid){
      this._accountService.EditItem(this.editForm?.value).subscribe({
        next: () => {
          this.toastr.success('Edit successful'),
          this.router.navigateByUrl('/item')
        },
        error: error => console.log(error)
      })
    }
  }
}
