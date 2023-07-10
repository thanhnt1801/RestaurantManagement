import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';
import {Location} from 'src/app/_models/location'
import { Status } from 'src/app/_models/status';
import { map } from 'rxjs';

@Component({
  selector: 'app-add-guest-table',
  templateUrl: './add-guest-table.component.html',
  styleUrls: ['./add-guest-table.component.scss'],
})
export class AddGuestTableComponent {
  addForm: FormGroup = new FormGroup({});
  currentRestaurant!: Restaurant | null;
  displayLocation: Location[] = [];
  displayStatus: Status[] = [];

  constructor(
    private _accountService: AccountService,
    private toastr: ToastrService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this._accountService.currentRestaurant$.subscribe({
      next: (reponse) => {
        this.currentRestaurant = reponse;
        setTimeout(() => {
          this.addForm.setValue({
            restaurantId: reponse?.id,
            locationId: 0,
            statusId: 0,
            name: '',
            description: ''
          });
        });
      },
      error: (error) => console.log(error),
    });

    if(!this.currentRestaurant?.id || this.currentRestaurant.id < 1)
    {
      this.router.navigateByUrl('/');
    };
    this.loadLocations();
    this.loadStatuses();
    this.initializeForm();
  }

  loadLocations(){
    this._accountService.getAllLocation().subscribe({
      next: response => {
        this.displayLocation = response.filter(
          (location) => location.restaurantId === this.currentRestaurant?.id)
      }
    })
  }

  onLocationChange(event: Location){
    this.addForm.get('locationId')?.setValue(event);
  }

  loadStatuses(){
    this._accountService.getAllStatus().subscribe({
      next: response => {
        this.displayStatus = response.filter(
          (status) => status.restaurantId === this.currentRestaurant?.id)
      }
    })
  }

  onStatusChange(event: Status){
    this.addForm.get('statusId')?.setValue(event);
  }

  initializeForm(){
    this.addForm = this.fb.group({
      restaurantId: [],
      locationId: ['', Validators.required],
      statusId: ['', Validators.required],
      name: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  onCreateGuestTable(){
    if(this.addForm?.valid)
    {
      this._accountService.CreateGuestTable(this.addForm?.value).subscribe({
        next: _ => {
          this.toastr.success('Created Successful!');
          this.router.navigateByUrl('/guest-table');
        },
          error: error => console.log(error),
      });
    }
  }
}
