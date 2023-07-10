import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NavComponent } from './components/nav/nav.component'
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { RestaurantComponent } from './components/restaurant/restaurant.component';
import { UserComponent } from './components/user/user.component'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './_modules/shared.module';
import { TestErrorComponent } from './components/test-error/test-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { LoginComponent } from './components/login/login.component';
import { AddRestaurantComponent } from './components/restaurant/add-restaurant/add-restaurant.component';
import { EditRestaurantComponent } from './components/restaurant/edit-restaurant/edit-restaurant.component';
import { EditUserComponent } from './components/user/edit-user/edit-user.component';
import { ModifyUserRoleComponent } from './components/user/modify-user-role/modify-user-role.component';
import { RoleComponent } from './components/role/role.component';
import { AddRoleComponent } from './components/role/add-role/add-role.component';
import { EditRoleComponent } from './components/role/edit-role/edit-role.component';
import { StatusComponent } from './components/status/status.component';
import { AddStatusComponent } from './components/status/add-status/add-status.component';
import { EditStatusComponent } from './components/status/edit-status/edit-status.component';
import { LocationComponent } from './components/location/location.component';
import { AddLocationComponent } from './components/location/add-location/add-location.component';
import { EditLocationComponent } from './components/location/edit-location/edit-location.component';
import { GuestTableComponent } from './components/guest-table/guest-table.component';
import { AddGuestTableComponent } from './components/guest-table/add-guest-table/add-guest-table.component';
import { EditGuestTableComponent } from './components/guest-table/edit-guest-table/edit-guest-table.component';
import { UnitComponent } from './components/unit/unit.component';
import { AddUnitComponent } from './components/unit/add-unit/add-unit.component';
import { EditUnitComponent } from './components/unit/edit-unit/edit-unit.component';
import { SizeComponent } from './components/size/size.component';
import { AddSizeComponent } from './components/size/add-size/add-size.component';
import { EditSizeComponent } from './components/size/edit-size/edit-size.component';
import { CategoryComponent } from './components/category/category.component';
import { AddCategoryComponent } from './components/category/add-category/add-category.component';
import { EditCategoryComponent } from './components/category/edit-category/edit-category.component';
import { ItemComponent } from './components/item/item.component';
import { AddItemComponent } from './components/item/add-item/add-item.component';
import { EditItemComponent } from './components/item/edit-item/edit-item.component';
import { PriceComponent } from './components/price/price.component';
import { AddPriceComponent } from './components/price/add-price/add-price.component';
import { EditPriceComponent } from './components/price/edit-price/edit-price.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    RestaurantComponent,
    UserComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    LoginComponent,
    AddRestaurantComponent,
    EditRestaurantComponent,
    EditUserComponent,
    ModifyUserRoleComponent,
    RoleComponent,
    AddRoleComponent,
    EditRoleComponent,
    StatusComponent,
    AddStatusComponent,
    EditStatusComponent,
    LocationComponent,
    AddLocationComponent,
    EditLocationComponent,
    GuestTableComponent,
    AddGuestTableComponent,
    EditGuestTableComponent,
    UnitComponent,
    AddUnitComponent,
    EditUnitComponent,
    SizeComponent,
    AddSizeComponent,
    EditSizeComponent,
    CategoryComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    ItemComponent,
    AddItemComponent,
    EditItemComponent,
    PriceComponent,
    AddPriceComponent,
    EditPriceComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,    
    SharedModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
