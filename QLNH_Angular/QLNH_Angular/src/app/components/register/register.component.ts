import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from 'src/app/_services/account.service';
import {Role} from 'src/app/_models/role'
import { Restaurant } from 'src/app/_models/restaurant';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  @Output() CancelRegister = new EventEmitter();
  model: any = {};
  displayRole: Role[] = [];
  displayRestaurant: Restaurant[] = [];

  registerForm: FormGroup = new FormGroup({})

  constructor(private _accountService : AccountService) {
  }

  ngOnInit(): void{
    this.getAllRestaurant();
    this.getAllRole();
    this.initializeForm();
  }

  initializeForm(){
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required,
        Validators.minLength(6), Validators.maxLength(12)]),
      confirmPassword: new FormControl('', [Validators.required,
        this.matchvalues('password')]),
      roleId: new FormControl('', Validators.required),
      restaurantId: new FormControl('', Validators.required)
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    });
  }

  matchvalues(matchTo: string) : ValidatorFn{
    return (control : AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {noMatching: true}
    }
  }

  getAllRestaurant(){
    this._accountService.getAllRestaurant().subscribe({
      next : response => {
        console.log('AllRestaurant: ',response);
        this.displayRestaurant = response
      },
      error: error => console.log('AllRestaurantError: ',error)
    })
  }

  getAllRole(){
    this._accountService.getAllRole().subscribe({
      next : response => {
        this.displayRole = response.filter((role) => {
          return role.deleted === false
        });
      },
      error: error => console.log('AllRoleError: ', error)
    })
  }

  onRoleChange(event : any){
    console.log('before change: ', this.registerForm.get('roleId')?.value);
    this.registerForm.get('role')?.setValue(event);
    console.log('after change: ', this.registerForm.get('roleId')?.value);
  }

  onRestaurantChange(event : any){
    this.registerForm.get('restaurant')?.setValue(event);
  }

  register(){
    console.log(this.registerForm);
    this._accountService.register(this.registerForm?.value).subscribe({
      next: response => console.log('register Success', response),
      error: error => console.log(error)
    })
  }

  cancel()
  {
    this.CancelRegister.emit(false);
  }
}
