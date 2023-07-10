import { Restaurant } from "./restaurant";
import { Size } from "./size";
import { Price } from "./price";

export interface Unit{
    id: number,
    name: string,
    description: string,
    created: Date,
    updated: Date,
    deleted: boolean,
    restaurantId: number,
    restaurant: Restaurant,
    sizes: Size[],
    prices: Price[]
}