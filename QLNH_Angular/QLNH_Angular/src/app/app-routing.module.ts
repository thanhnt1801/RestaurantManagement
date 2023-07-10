import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { UserComponent } from './components/user/user.component';
import { RestaurantComponent } from './components/restaurant/restaurant.component';
import { authGuard } from './_guards/auth.guard';
import { roleGuard } from './_guards/role.guard';
import { TestErrorComponent } from './components/test-error/test-error.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
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
import { PriceComponent } from './components/price/price.component';
import { AddPriceComponent } from './components/price/add-price/add-price.component';
import { EditPriceComponent } from './components/price/edit-price/edit-price.component';
import { ItemComponent } from './components/item/item.component';
import { AddItemComponent } from './components/item/add-item/add-item.component';
import { EditItemComponent } from './components/item/edit-item/edit-item.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: '', 
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'user', component: UserComponent},
      { path: 'restaurant', component: RestaurantComponent},
      { path: 'role', component: RoleComponent},
      { path: 'status', component: StatusComponent},
      { path: 'location', component: LocationComponent},
      { path: 'guest-table', component: GuestTableComponent},
      { path: 'unit', component: UnitComponent},
      { path: 'size', component: SizeComponent},
      { path: 'category', component: CategoryComponent},
      { path: 'price', component: PriceComponent},
      { path: 'item', component: ItemComponent},
      { path: 'add-status', component: AddStatusComponent},
      { path: 'edit-status/:id', component: EditStatusComponent},
      { path: 'add-location', component: AddLocationComponent},
      { path: 'edit-location/:id', component: EditLocationComponent},
      { path: 'add-guest-table', component: AddGuestTableComponent},
      { path: 'edit-guest-table/:id', component: EditGuestTableComponent},
      { path: 'add-unit', component: AddUnitComponent},
      { path: 'edit-unit/:id', component: EditUnitComponent},
      { path: 'add-size', component: AddSizeComponent},
      { path: 'edit-size/:id', component: EditSizeComponent},
      { path: 'add-category', component: AddCategoryComponent},
      { path: 'edit-category/:id', component: EditCategoryComponent},
      { path: 'add-price', component: AddPriceComponent},
      { path: 'edit-price/:id', component: EditPriceComponent},
      { path: 'add-item', component: AddItemComponent},
      { path: 'edit-item/:id', component: EditItemComponent},
    ]
  },
  { path: '', 
    runGuardsAndResolvers: 'always',
    canActivate: [roleGuard],
    children: [
      { path: 'add-restaurant', component: AddRestaurantComponent},
      { path: 'edit-restaurant/:id', component: EditRestaurantComponent},
      { path: 'edit-user/:id', component: EditUserComponent},
      { path: 'modify-user-role/:id', component: ModifyUserRoleComponent},
      { path: 'add-role', component: AddRoleComponent},
      { path: 'edit-role/:id', component: EditRoleComponent},
    ]
  },
  { path: 'error', component: TestErrorComponent},
  { path: 'not-found', component: NotFoundComponent},
  { path: 'server-error', component: ServerErrorComponent},
  { path: '**', component: NotFoundComponent, pathMatch: 'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
