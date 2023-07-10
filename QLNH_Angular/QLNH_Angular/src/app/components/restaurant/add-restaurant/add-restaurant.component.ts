import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-restaurant',
  templateUrl: './add-restaurant.component.html',
  styleUrls: ['./add-restaurant.component.scss']
})
export class AddRestaurantComponent {
  addForm: FormGroup = new FormGroup({});
  constructor(private _accountService:AccountService, 
    private toastr:ToastrService, 
    private router : Router,
    private fb : FormBuilder){}

  public ngOnInit()
  {
    this.initializaForm();
  }

  initializaForm(){
    this.addForm = new FormGroup({
      name: new FormControl('', Validators.required),
      phone: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      address: new FormControl('', Validators.required),
    });
  }

  onCreateRestaurant()
  {
    if(this,this.addForm.valid)
    {
      this._accountService.CreateRestaurant(this.addForm?.value).subscribe({
        next: _ => this.toastr.success('Created Successful!'),
        error: error => console.log(error),
        complete: () => this.router.navigateByUrl('restaurant')
      })
    }
  }
}
