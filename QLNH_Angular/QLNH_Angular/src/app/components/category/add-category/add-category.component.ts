import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/_models/category';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';
@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss']
})
export class AddCategoryComponent {
  addForm: FormGroup = new FormGroup({});
  currentRestaurant!: Restaurant | null;
  displayCategory: Category[] = [];

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
            parentId: 0
          });
        });
      },
      error: error => console.log(error)
    });

    if(!this.currentRestaurant?.id || this.currentRestaurant.id < 1)
    {
      this.router.navigateByUrl('/');
    };
    this.loadCategory();
    this.initializeForm();
  }

  loadCategory(){
    this._accountService.getAllCategory().subscribe({
      next: response => {
        this.displayCategory = response.filter(
          category => category.restaurantId === this.currentRestaurant?.id 
        );
      },
      error: error => console.log(error)
    })
  }

  initializeForm(){
    this.addForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      restaurantId: [],
      parentId: []
    });
  }

  onCreateCategory(){
    if(this.addForm.valid)
    {
      this._accountService.CreateCategory(this.addForm?.value).subscribe({
        next: _ => {
          this.toastr.success('Created Successful!');
          this.router.navigateByUrl('category');
        },
          error: error => console.log(error),
      })
    }
  }

  onCategoryChange(event : Category)
  {
    this.addForm.get('parentId') ?.setValue(event);
  }
}
