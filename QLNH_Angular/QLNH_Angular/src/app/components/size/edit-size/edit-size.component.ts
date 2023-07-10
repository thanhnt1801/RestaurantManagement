import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Restaurant } from 'src/app/_models/restaurant';
import { Unit } from 'src/app/_models/unit';
import { AccountService } from 'src/app/_services/account.service';
@Component({
  selector: 'app-edit-size',
  templateUrl: './edit-size.component.html',
  styleUrls: ['./edit-size.component.scss']
})
export class EditSizeComponent {
  editForm: FormGroup = new FormGroup({});
  id : number = 0;
  displayUnit: Unit[] = [];
  currentRestaurant!: Restaurant | null;

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
    this.getSizeToEdit();
    this.loadUnits();
    this.initializeForm();
  }

  getSizeToEdit(){
    this._accountService.getSize(this.id).subscribe({
      next: response => {
        this.editForm.setValue({
          name: response.name,
          description: response.description,
          id: response.id,
          unitId: response.unitId
        });
      },
      error: error => console.log(error)
    })
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
    this.editForm.get('unitId')?.setValue(event);
  }

  initializeForm(){
    this.editForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      unitId: ['', Validators.required],
      id: []
    });
  }

  onEditSize(){
    if(this.editForm?.valid){
      this._accountService.EditSize(this.editForm?.value).subscribe({
        next: () => {
          this.toastr.success('Edit successful'),
          this.router.navigateByUrl('/unit')
        },
        error: error => console.log(error)
      })
    }
  }
}
