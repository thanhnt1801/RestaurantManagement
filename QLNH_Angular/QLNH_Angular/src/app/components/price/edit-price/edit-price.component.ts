import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';
import { Restaurant } from 'src/app/_models/restaurant';
import { Size } from 'src/app/_models/size';
import { Unit } from 'src/app/_models/unit';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-edit-price',
  templateUrl: './edit-price.component.html',
  styleUrls: ['./edit-price.component.scss']
})
export class EditPriceComponent {
  editForm: FormGroup = new FormGroup({});
  id : number = 0;
  currentRestaurant!: Restaurant | null;
  displayUnit: Unit[] = [];
  selectedUnitId: any;
  displaySize: Size[] = [];
  displayItem: Item[] = [];
  
  constructor(private _accountService : AccountService,
      private toastr : ToastrService,
      private fb: FormBuilder,
      private actRoute: ActivatedRoute,
      private router: Router){}

  ngOnInit(){
    this._accountService.currentRestaurant$.subscribe({
      next: response => {
        this.currentRestaurant = response;
      },
      error: error => console.log(error)
    });

    if(!this.currentRestaurant?.id || this.currentRestaurant.id < 1)
    {
      this.router.navigateByUrl('/');
    };

    this.id = +this.actRoute.snapshot.paramMap.get('id')!;
    this.getPriceToEdit();
    this.loadItems();
    this.loadUnits();
    this.initializeForm();
  }

  getPriceToEdit(){
    this._accountService.getPrice(this.id).subscribe({
      next: response => {
        console.log('Edit Form: ', response);

        this.editForm.setValue({
          description: response.description,
          id: response.id,
          unitId: response.unitId,
          sizeId: response.sizeId,
          itemId: response.itemId,
          salePrice: response.salePrice
        });
      },
      error: error => console.log(error)
    })
  }

  loadUnits(){
    this._accountService.getAllUnit().subscribe({
      next: response => {
        this.displayUnit = response.filter(
          (unit) => unit.restaurantId === this.currentRestaurant?.id
            && unit.deleted === false);
      }
    })
  }

  loadItems(){
    this._accountService.getAllItem().subscribe({
      next: response => {
        this.displayItem = response.filter(
          (item) => {
            return item.restaurantId === this.currentRestaurant?.id 
              && item.deleted === false
          });
      }
    })
  }

  loadSizes(unitId: number){
    this._accountService.getAllSize().subscribe({
      next: response => {
        this.displaySize = response.filter(
          (size) => {
            return size.restaurantId === this.currentRestaurant?.id &&
              size.unitId === unitId && size.deleted === false
          });
      }
    })
  }

  onUnitChange(event: Unit){
    this.editForm.get('unitId')?.setValue(event);
    this.selectedUnitId = event;
    this.loadSizes(this.selectedUnitId);
  }

  onSizeChange(event: Size){
    this.editForm.get('sizeId')?.setValue(event);
  }

  onItemChange(event: Item){
    this.editForm.get('itemId')?.setValue(event);
  }

  initializeForm(){
    this.editForm = this.fb.group({
      salePrice: ['', Validators.required],
      description: ['', Validators.required],
      unitId: ['', Validators.required],
      sizeId: ['', Validators.required],
      itemId: ['', Validators.required],
      id: []
    });
  }

  onEditPrice(){
    if(this.editForm?.valid){
      this._accountService.EditPrice(this.editForm?.value).subscribe({
        next: () => {
          this.toastr.success('Edit successful'),
          this.router.navigateByUrl('/item')
        },
        error: error => console.log(error)
      })
    }
  }
}
