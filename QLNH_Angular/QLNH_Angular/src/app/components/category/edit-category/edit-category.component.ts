import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/_models/category';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.scss']
})
export class EditCategoryComponent {
  editForm: FormGroup = new FormGroup({});
  id : number = 0;
  displayCategory: Category[] = [];
  currentRestaurant!: Restaurant | null;
  checkCategory!: Category | null;

  constructor(private _accountService : AccountService,
      private toastr : ToastrService,
      private fb: FormBuilder,
      private actRoute: ActivatedRoute,
      private router: Router){}

  ngOnInit(){
    this._accountService.currentRestaurant$.subscribe({
      next: response => this.currentRestaurant = response,
      error: error => console.log(error)
    });

    if(!this.currentRestaurant?.id || this.currentRestaurant.id < 1)
    {
      this.router.navigateByUrl('/');
    };

    this.id = +this.actRoute.snapshot.paramMap.get('id')!;
    this.getCategoryToEdit();
    this.loadCategory();
    this.initializeForm();
  }

  getCategoryToEdit(){
    this._accountService.getCategory(this.id).subscribe({
      next: response => {
        this.editForm.setValue({
          name: response.name,
          description: response.description,
          id: response.id,
          parentId: response.parentId
        });
        this.checkCategory = response;
      },
      error: error => console.log(error)
    })
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
    this.editForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      id: [],
      parentId: []
    });
  }

  onEditCategory(){
    console.log('edit Form parent: ', this.editForm.value);
    const parentId  = Number(this.editForm.get('parentId'));
    if(!this.checkParentId(parentId))
    {
      if(this.editForm?.valid){
        this._accountService.EditCategory(this.editForm?.value).subscribe({
          next: () => {
            this.toastr.success('Edit successful'),
            this.router.navigateByUrl('/category')
          },
          error: error => console.log(error)
        })
      }
    }
    
  }

  checkParentId(parentId : number) : Category{ //Vi du: Id cua A la 1, children cua A la B, kiem tra ko cho phep A la con cua B.
    const checkDuplicateId = this.checkCategory?.children.find(
      (children) => children.id === parentId
    );
    return checkDuplicateId!;
  }

  onCategoryChange(event: Category){
    this.editForm.get('parentId')?.setValue(event);
  }
}
