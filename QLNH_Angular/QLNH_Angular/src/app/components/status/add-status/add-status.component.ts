import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-add-status',
  templateUrl: './add-status.component.html',
  styleUrls: ['./add-status.component.scss']
})
export class AddStatusComponent {
  addForm: FormGroup = new FormGroup({});
  currentRestaurant!: Restaurant | null;

  constructor(private _accountService:AccountService, 
    private toastr:ToastrService, 
    private router : Router,
    private fb : FormBuilder){}

  ngOnInit(){
    this._accountService.currentRestaurant$.subscribe({
      next: response => {
        this.currentRestaurant = response;
        setTimeout(() => {
          this.addForm.setValue({
            restaurantId: response?.id,
            name: '',
            description: ''
          });
        });
      },
      error: error => console.log(error)
    });

    if(!this.currentRestaurant?.id || this.currentRestaurant.id < 1)
    {
      this.router.navigateByUrl('/');
    };

    this.initializeForm();
  }

  initializeForm(){
    this.addForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      restaurantId: []
    });
  }

  onCreateStatus(){
    if(this.addForm.valid)
    {
      this._accountService.CreateStatus(this.addForm?.value).subscribe({
        next: _ => {
          this.toastr.success('Created Successful!');
          this.router.navigateByUrl('status');
        },
          error: error => console.log(error),
      })
    }
  }
}
