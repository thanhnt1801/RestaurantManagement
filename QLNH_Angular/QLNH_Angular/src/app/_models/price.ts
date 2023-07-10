import { Item } from "./item";
import { Restaurant } from "./restaurant";
import { Size } from "./size";
import { Unit } from "./unit";

export interface Price{
    id: number,
    salePrice: number,
    description: string,
    created: Date,
    updated: Date,
    deleted: boolean,
    restaurantId: number,
    restaurant: Restaurant,
    unitId: number,
    unit: Unit,
    sizeId: number,
    size: Size,
    itemId: number,
    items: Item
}