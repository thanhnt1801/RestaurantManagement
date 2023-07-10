import { GuestTable } from "./guestTable"
import { Restaurant } from "./restaurant"

export interface Status{
    id: number,
    name: string,
    description: string,
    created: Date,
    updated: Date,
    deleted: boolean,
    restaurantId: number,
    restaurant: Restaurant,
    guestTable: GuestTable[]
}