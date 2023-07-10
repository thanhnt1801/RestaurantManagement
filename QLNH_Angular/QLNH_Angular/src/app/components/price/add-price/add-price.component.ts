import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';
import { Restaurant } from 'src/app/_models/restaurant';
import { Size } from 'src/app/_models/size';
import { Unit } from 'src/app/_models/unit';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-add-price',
  templateUrl: './add-price.component.html',
  styleUrls: ['./add-price.component.scss']
})
export class AddPriceComponent {
  addForm: FormGroup = new FormGroup({});
  currentRestaurant!: Restaurant | null;
  displayUnit: Unit[] = [];
  selectedUnitId: any;
  displaySize: Size[] = [];
  displayItem: Item[] = [];


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
            description: '',
            unitId: 0,
            sizeId: 0,
            itemId: 0,
            salePrice: 0
          });
        });
      },
      error: error => console.log(error)
    });

    if(!this.currentRestaurant?.id || this.currentRestaurant.id < 1)
    {
      this.router.navigateByUrl('/');
    };
    this.loadItems();
    this.loadUnits();
    this.initializeForm();
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
    this.addForm.get('unitId')?.setValue(event);
    this.selectedUnitId = event;
    this.loadSizes(this.selectedUnitId);
  }

  onSizeChange(event: Size){
    this.addForm.get('sizeId')?.setValue(event);
  }

  onItemChange(event: Item){
    this.addForm.get('itemId')?.setValue(event);
  }


  initializeForm(){
    this.addForm = this.fb.group({
      salePrice: ['', Validators.required],
      description: ['', Validators.required],
      unitId: ['', Validators.required],
      sizeId: ['', Validators.required],
      itemId: ['', Validators.required],
      restaurantId: []
    });
  }

  onCreatePrice(){
    if(this.addForm.valid)
    {
      this._accountService.CreatePrice(this.addForm?.value).subscribe({
        next: _ => {
          this.toastr.success('Created Successful!');
          this.router.navigateByUrl('item');
        },
          error: error => console.log(error),
      })
    }
  }
}
