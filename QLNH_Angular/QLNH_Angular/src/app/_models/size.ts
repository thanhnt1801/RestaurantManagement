import { Price } from "./price";
import { Restaurant } from "./restaurant";
import { Unit } from "./unit";

export interface Size{
    id: number,
    name: string,
    description: string,
    created: Date,
    updated: Date,
    deleted: boolean,
    restaurantId: number,
    restaurant: Restaurant,
    unitId: number,
    unit: Unit,
    prices: Price[]
}