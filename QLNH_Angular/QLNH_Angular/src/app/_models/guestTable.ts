import { Restaurant } from "./restaurant";
import {Location} from "./location"
import { Status } from "./status";

export interface GuestTable{
    id: number,
    name: string,
    description: string,
    created: Date,
    updated: Date,
    deleted: boolean,
    restaurantId: number,
    restaurant: Restaurant,
    locationId: number,
    location: Location,
    statusId: number,
    status:  Status
}