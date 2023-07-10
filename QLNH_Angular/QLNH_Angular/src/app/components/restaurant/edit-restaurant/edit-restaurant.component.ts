import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-edit-restaurant',
  templateUrl: './edit-restaurant.component.html',
  styleUrls: ['./edit-restaurant.component.scss']
})
export class EditRestaurantComponent {
  editForm: FormGroup = new FormGroup({});
  id : number = 0;
  constructor(private _accountService : AccountService,
      private toastr : ToastrService,
      private fb: FormBuilder,
      private actRoute: ActivatedRoute,
      private router: Router){}

  ngOnInit(){
    this.id = +this.actRoute.snapshot.paramMap.get('id')!; //hoac co the cast = Number(...)
    this.getRestaurantToEdit();
    this.initializaForm();
  }

  getRestaurantToEdit()
  {
    this._accountService.getRestaurant(this.id).subscribe({
      next: response => {
        this.editForm.setValue({
          name: response.name,
          phone: response.phone,
          address: response.address,
          description: response.description,
          id: response.id
        })
      },
      error: error => console.log(error)
    })
  }

  initializaForm(){
    this.editForm = this.fb.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
      description: ['', Validators.required],
      address: ['', Validators.required],
      id : []
    });
  }

  onEditRestaurant(){
    if(this.editForm.valid)
    {
      this._accountService.EditRestaurant(this.editForm?.value).subscribe({
        next: () => {
          this.toastr.success('Edit restaurant successful'),
          this.router.navigateByUrl('/restaurant')
        },
        error: error => console.log(error)
      })
    }

  }
}
