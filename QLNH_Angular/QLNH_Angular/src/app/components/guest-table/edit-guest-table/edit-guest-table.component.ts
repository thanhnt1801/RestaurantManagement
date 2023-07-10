import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Status } from 'src/app/_models/status';
import { AccountService } from 'src/app/_services/account.service';
import {Location} from 'src/app/_models/location'
import { Restaurant } from 'src/app/_models/restaurant';

@Component({
  selector: 'app-edit-guest-table',
  templateUrl: './edit-guest-table.component.html',
  styleUrls: ['./edit-guest-table.component.scss']
})
export class EditGuestTableComponent {
  editForm: FormGroup = new FormGroup({});
  id : number = 0;
  displayLocation: Location[] = [];
  displayStatus: Status[] = [];
  currentRestaurant!: Restaurant | null;
  
  constructor(private _accountService : AccountService,
      private toastr : ToastrService,
      private fb: FormBuilder,
      private actRoute: ActivatedRoute,
      private router: Router){}

  ngOnInit(){
    this._accountService.currentRestaurant$.subscribe({
      next: (reponse) => {
        this.currentRestaurant = reponse;
      },
      error: error => console.log(error)
    });

    if(!this.currentRestaurant?.id || this.currentRestaurant.id < 1)
    {
      this.router.navigateByUrl('/');
    };

    this.id = +this.actRoute.snapshot.paramMap.get('id')!;
    this.getGuestTableToEdit();
    this.loadLocations();
    this.loadStatuses();
    this.initializeForm();
  }

  getGuestTableToEdit(){
    this._accountService.getGuestTable(this.id).subscribe({
      next: response => {
        this.editForm.setValue({
          name: response.name,
          description: response.description,
          locationId: response.locationId,
          statusId: response.statusId,
          id: response.id
        });
      },
      error: error => console.log(error)
    })
  }

  loadLocations(){
    this._accountService.getAllLocation().subscribe({
      next: response => {
        this.displayLocation = response.filter(
          (location) => location.restaurantId === this.currentRestaurant?.id)
      }
    })
  }

  loadStatuses(){
    this._accountService.getAllStatus().subscribe({
      next: response => {
        this.displayStatus = response.filter(
          (status) => status.restaurantId === this.currentRestaurant?.id)
      }
    })
  }

  initializeForm(){
    this.editForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      locationId: ['', Validators.required],
      statusId: ['', Validators.required],
      id: [],
    });
  }

  onLocationChange(event: Location){
    this.editForm.get('locationId')?.setValue(event);
  }

  onStatusChange(event: Status){
    this.editForm.get('statusId')?.setValue(event);
  }

  onEditGuestTable(){
    if(this.editForm?.valid){
      this._accountService.EditGuestTable(this.editForm?.value).subscribe({
        next: () => {
          this.toastr.success('Edit successful'),
          this.router.navigateByUrl('/guest-table')
        },
        error: error => console.log(error)
      })
    }
  }
}
