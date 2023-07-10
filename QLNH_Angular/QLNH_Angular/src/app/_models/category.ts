import { Item } from "./item";
import { Restaurant } from "./restaurant";

export interface Category{
    id: number,
    name: string,
    description: string,
    created: Date,
    updated: Date,
    deleted: boolean,
    restaurantId: number,
    restaurant: Restaurant,
    parentId: number,
    parent: Category,
    children: Category[],
    items: Item[]
}