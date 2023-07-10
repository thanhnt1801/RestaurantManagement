import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { User } from '../_models/user';
import { Restaurant } from '../_models/restaurant';
import { Personnel } from '../_models/personnel';
import { Role } from '../_models/role';
import { Status } from '../_models/status';
import { Location } from '../_models/location';
import { GuestTable } from '../_models/guestTable';
import { Unit } from '../_models/unit';
import { Size } from '../_models/size';
import { Price } from '../_models/price';
import { Category } from '../_models/category';
import { Item } from '../_models/item';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:7251/api/';
  
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  public currentRestaurantSource = new BehaviorSubject<Restaurant | null>(null);
  currentRestaurant$ = this.currentRestaurantSource.asObservable();

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-type': 'application/json',
      // Authorization: 'my-auth-token'
    }),
  };

  constructor(private http:HttpClient) { }

  login(model : any) {
    return this.http.post<User>(this.baseUrl + 'Authentication/login', model).pipe(
      map((response: User) => {
        const user = response;
        if(user)
        {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(model : any)
  {
    return this.http.post<User>(this.baseUrl + 'Authentication/register', model).pipe(
      map(user => {
        if(user)
        {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  SetCurrentUser(user : User)
  {
    this.currentUserSource.next(user);
  }

  getRoleFromJwt() : boolean{
    const token = localStorage.getItem('user');
    const decodedToken : any = jwt_decode(token!);
    const role : string = decodedToken.role;
    if(role === "Admin")
    {
      return true;
    }
    return false;
  }


  logout()
  {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.currentRestaurantSource.next(null);

  }

  //Restaurant
  getAllRestaurant() : Observable<Restaurant[]>{
    return this.http.get<Restaurant[]>(this.baseUrl + 'Restaurants', this.httpOptions);
  }

  getRestaurant(restaurantId : number) : Observable<Restaurant>{
    return this.http.get<Restaurant>(this.baseUrl + 'Restaurants/' + restaurantId, this.httpOptions);
  }

  CreateRestaurant(payload : Restaurant) : Observable<Restaurant>{
    return this.http.post<Restaurant>(this.baseUrl + 'Restaurants', payload, this.httpOptions);
  }

  EditRestaurant(payload : Restaurant) : Observable<Restaurant>{
    return this.http.put<Restaurant>(this.baseUrl + 'Restaurants', payload, this.httpOptions);
  }

  DeleteRestaurant(restaurantId: number) : Observable<Restaurant>{
    return this.http.delete<Restaurant>(this.baseUrl + 'Restaurants/' + restaurantId, this.httpOptions)
  }

  RestoreRestaurant(restaurantId: number) : Observable<Restaurant>{
    return this.http.put<Restaurant>(this.baseUrl + 'Restaurants/RestoreRestaurant/' + restaurantId, this.httpOptions)
  }
  
  //User
  getAllUser() : Observable<Personnel[]>{
    return this.http.get<Personnel[]>(this.baseUrl + 'Users', this.httpOptions);
  }

  getUser(userId : number) : Observable<Personnel>{
    return this.http.get<Personnel>(this.baseUrl + 'Users/' + userId, this.httpOptions);
  }

  CreateUser(payload : Personnel) : Observable<Personnel>{
    return this.http.post<Personnel>(this.baseUrl + 'Users', payload, this.httpOptions);
  }

  EditUser(payload : Personnel) : Observable<Personnel>{
    return this.http.put<Personnel>(this.baseUrl + 'Users', payload, this.httpOptions);
  }

  DeleteUser(userId: number) : Observable<Personnel>{
    return this.http.delete<Personnel>(this.baseUrl + 'Users/' + userId, this.httpOptions)
  }

  RestoreUser(userId: number) : Observable<Personnel>{
    return this.http.put<Personnel>(this.baseUrl + 'Users/RestoreUser/' + userId, this.httpOptions)
  }

  AddUserToRestaurant(restaurantId: number, userId : number) : Observable<Personnel>{
    return this.http.put<Personnel>(this.baseUrl + 'Users/AddUserToRestaurant?restaurantId=' + restaurantId + '&userId=' + userId, this.httpOptions)
  }

  AddUserToRole(payload: Personnel) : Observable<Personnel>{
    return this.http.put<Personnel>(this.baseUrl + 'Users/AddUserToRole?roleId=' + payload.roleId + '&userId=' + payload.id, this.httpOptions)
  }

  //Role
  getAllRole() : Observable<Role[]>{
    return this.http.get<Role[]>(this.baseUrl + 'Roles', this.httpOptions);
  }

  getRole(roleId : number) : Observable<Role>{
    return this.http.get<Role>(this.baseUrl + 'Roles/' + roleId, this.httpOptions);
  }

  CreateRole(payload : Role) : Observable<Role>{
    return this.http.post<Role>(this.baseUrl + 'Roles', payload, this.httpOptions);
  }

  EditRole(payload : Role) : Observable<Role>{
    return this.http.put<Role>(this.baseUrl + 'Roles', payload, this.httpOptions);
  }

  DeleteRole(roleId: number) : Observable<Role>{
    return this.http.delete<Role>(this.baseUrl + 'Roles/' + roleId, this.httpOptions)
  }

  RestoreRole(roleId: number) : Observable<Role>{
    return this.http.put<Role>(this.baseUrl + 'Roles/RestoreRole/' + roleId, this.httpOptions)
  }

  //Status
  getAllStatus() : Observable<Status[]>{
    return this.http.get<Status[]>(this.baseUrl + 'Statuses', this.httpOptions);
  }

  getStatus(statusId : number) : Observable<Status>{
    return this.http.get<Status>(this.baseUrl + 'Statuses/' + statusId, this.httpOptions);
  }

  CreateStatus(payload : Status) : Observable<Status>{
    return this.http.post<Status>(this.baseUrl + 'Statuses', payload, this.httpOptions);
  }

  EditStatus(payload : Status) : Observable<Status>{
    return this.http.put<Status>(this.baseUrl + 'Statuses', payload, this.httpOptions);
  }

  DeleteStatus(statusId: number) : Observable<Status>{
    return this.http.delete<Status>(this.baseUrl + 'Statuses/' + statusId, this.httpOptions)
  }

  RestoreStatus(statusId: number) : Observable<Status>{
    return this.http.put<Status>(this.baseUrl + 'Statuses/RestoreStatus/' + statusId, this.httpOptions)
  }

  //Location
  getAllLocation() : Observable<Location[]>{
    return this.http.get<Location[]>(this.baseUrl + 'Locations', this.httpOptions);
  }

  getLocation(locationId : number) : Observable<Location>{
    return this.http.get<Location>(this.baseUrl + 'Locations/' + locationId, this.httpOptions);
  }

  CreateLocation(payload : Location) : Observable<Location>{
    return this.http.post<Location>(this.baseUrl + 'Locations', payload, this.httpOptions);
  }

  EditLocation(payload : Location) : Observable<Location>{
    return this.http.put<Location>(this.baseUrl + 'Locations', payload, this.httpOptions);
  }

  DeleteLocation(locationId: number) : Observable<Location>{
    return this.http.delete<Location>(this.baseUrl + 'Locations/' + locationId, this.httpOptions)
  }

  RestoreLocation(locationId: number) : Observable<Location>{
    return this.http.put<Location>(this.baseUrl + 'Locations/RestoreLocation/' + locationId, this.httpOptions)
  }

  //GuestTable
  getAllGuestTable() : Observable<GuestTable[]>{
    return this.http.get<GuestTable[]>(this.baseUrl + 'GuestTables', this.httpOptions);
  }

  getGuestTable(guestTableId : number) : Observable<GuestTable>{
    return this.http.get<GuestTable>(this.baseUrl + 'GuestTables/' + guestTableId, this.httpOptions);
  }

  CreateGuestTable(payload : GuestTable) : Observable<GuestTable>{
    return this.http.post<GuestTable>(this.baseUrl + 'GuestTables', payload, this.httpOptions);
  }

  EditGuestTable(payload : GuestTable) : Observable<GuestTable>{
    return this.http.put<GuestTable>(this.baseUrl + 'GuestTables', payload, this.httpOptions);
  }

  DeleteGuestTable(guestTableId: number) : Observable<GuestTable>{
    return this.http.delete<GuestTable>(this.baseUrl + 'GuestTables/' + guestTableId, this.httpOptions)
  }

  RestoreGuestTable(guestTableId: number) : Observable<GuestTable>{
    return this.http.put<GuestTable>(this.baseUrl + 'GuestTables/RestoreGuestTable/' + guestTableId, this.httpOptions)
  }

  //Unit
  getAllUnit() : Observable<Unit[]>{
    return this.http.get<Unit[]>(this.baseUrl + 'Units', this.httpOptions);
  }

  getUnit(unitId : number) : Observable<Unit>{
    return this.http.get<Unit>(this.baseUrl + 'Units/' + unitId, this.httpOptions);
  }

  CreateUnit(payload : Unit) : Observable<Unit>{
    return this.http.post<Unit>(this.baseUrl + 'Units', payload, this.httpOptions);
  }

  EditUnit(payload : Unit) : Observable<Unit>{
    return this.http.put<Unit>(this.baseUrl + 'Units', payload, this.httpOptions);
  }

  DeleteUnit(unitId: number) : Observable<Unit>{
    return this.http.delete<Unit>(this.baseUrl + 'Units/' + unitId, this.httpOptions)
  }

  RestoreUnit(unitId: number) : Observable<Unit>{
    return this.http.put<Unit>(this.baseUrl + 'Units/RestoreUnit/' + unitId, this.httpOptions)
  }

  //Size
  getAllSize() : Observable<Size[]>{
    return this.http.get<Size[]>(this.baseUrl + 'Sizes', this.httpOptions);
  }

  getSize(sizeId : number) : Observable<Size>{
    return this.http.get<Size>(this.baseUrl + 'Sizes/' + sizeId, this.httpOptions);
  }

  CreateSize(payload : Size) : Observable<Size>{
    return this.http.post<Size>(this.baseUrl + 'Sizes', payload, this.httpOptions);
  }

  EditSize(payload : Size) : Observable<Size>{
    return this.http.put<Size>(this.baseUrl + 'Sizes', payload, this.httpOptions);
  }

  DeleteSize(sizeId: number) : Observable<Size>{
    return this.http.delete<Size>(this.baseUrl + 'Sizes/' + sizeId, this.httpOptions)
  }

  RestoreSize(sizeId: number) : Observable<Size>{
    return this.http.put<Size>(this.baseUrl + 'Sizes/RestoreSize/' + sizeId, this.httpOptions)
  }

  //Price
  getAllPrice() : Observable<Price[]>{
    return this.http.get<Price[]>(this.baseUrl + 'Prices', this.httpOptions);
  }

  getPrice(priceId : number) : Observable<Price>{
    return this.http.get<Price>(this.baseUrl + 'Prices/' + priceId, this.httpOptions);
  }

  CreatePrice(payload : Price) : Observable<Price>{
    return this.http.post<Price>(this.baseUrl + 'Prices', payload, this.httpOptions);
  }

  EditPrice(payload : Price) : Observable<Price>{
    return this.http.put<Price>(this.baseUrl + 'Prices', payload, this.httpOptions);
  }

  DeletePrice(priceId: number) : Observable<Price>{
    return this.http.delete<Price>(this.baseUrl + 'Prices/' + priceId, this.httpOptions)
  }

  RestorePrice(priceId: number) : Observable<Price>{
    return this.http.put<Price>(this.baseUrl + 'Prices/RestorePrice/' + priceId, this.httpOptions)
  }

  //Category
  getAllCategory() : Observable<Category[]>{
    return this.http.get<Category[]>(this.baseUrl + 'Categories', this.httpOptions);
  }

  getCategory(categoryId : number) : Observable<Category>{
    return this.http.get<Category>(this.baseUrl + 'Categories/' + categoryId, this.httpOptions);
  }

  CreateCategory(payload : Category) : Observable<Category>{
    return this.http.post<Category>(this.baseUrl + 'Categories', payload, this.httpOptions);
  }

  EditCategory(payload : Category) : Observable<Category>{
    return this.http.put<Category>(this.baseUrl + 'Categories', payload, this.httpOptions);
  }

  DeleteCategory(categoryId: number) : Observable<Category>{
    return this.http.delete<Category>(this.baseUrl + 'Categories/' + categoryId, this.httpOptions)
  }

  RestoreCategory(categoryId: number) : Observable<Category>{
    return this.http.put<Category>(this.baseUrl + 'Categories/RestoreCategory/' + categoryId, this.httpOptions)
  }

  //Item
  getAllItem() : Observable<Item[]>{
    return this.http.get<Item[]>(this.baseUrl + 'Items', this.httpOptions);
  }

  getItem(itemId : number) : Observable<Item>{
    return this.http.get<Item>(this.baseUrl + 'Items/' + itemId, this.httpOptions);
  }

  CreateItem(payload : Item) : Observable<Item>{
    return this.http.post<Item>(this.baseUrl + 'Items', payload, this.httpOptions);
  }

  EditItem(payload : Item) : Observable<Item>{
    return this.http.put<Item>(this.baseUrl + 'Items', payload, this.httpOptions);
  }

  DeleteItem(itemId: number) : Observable<Item>{
    return this.http.delete<Item>(this.baseUrl + 'Items/' + itemId, this.httpOptions)
  }

  RestoreItem(itemId: number) : Observable<Item>{
    return this.http.put<Item>(this.baseUrl + 'Items/RestoreItem/' + itemId, this.httpOptions)
  }
}
