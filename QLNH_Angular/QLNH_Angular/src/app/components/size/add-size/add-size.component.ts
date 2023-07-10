import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Restaurant } from 'src/app/_models/restaurant';
import { Unit } from 'src/app/_models/unit';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-add-size',
  templateUrl: './add-size.component.html',
  styleUrls: ['./add-size.component.scss']
})
export class AddSizeComponent {
  addForm: FormGroup = new FormGroup({});
  currentRestaurant!: Restaurant | null;
  displayUnit: Unit[] = [];

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
            description: '',
            unitId: 0
          });
        });
      },
      error: error => console.log(error)
    });

    if(!this.currentRestaurant?.id || this.currentRestaurant.id < 1)
    {
      this.router.navigateByUrl('/');
    };
    this.loadUnits();
    this.initializeForm();
  }

  loadUnits(){
    this._accountService.getAllUnit().subscribe({
      next: response => {
        this.displayUnit = response.filter(
          (unit) =>  {
            return unit.restaurantId === this.currentRestaurant?.id
              && unit.deleted === false;
          })
      }
    })
  }

  onUnitChange(event: Unit){
    this.addForm.get('unitId')?.setValue(event);
  }

  initializeForm(){
    this.addForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      unitId: ['', Validators.required],
      restaurantId: []
    });
  }

  onCreateSize(){
    if(this.addForm.valid)
    {
      this._accountService.CreateSize(this.addForm?.value).subscribe({
        next: _ => {
          this.toastr.success('Created Successful!');
          this.router.navigateByUrl('unit');
        },
          error: error => console.log(error),
      })
    }
  }
}
