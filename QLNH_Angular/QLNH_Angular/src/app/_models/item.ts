import { Category } from "./category";
import { Price } from "./price";
import { Restaurant } from "./restaurant";

export interface Item{
    id: number,
    name: string,
    description: string,
    created: Date,
    updated: Date,
    deleted: boolean,
    restaurantId: number,
    restaurant: Restaurant,
    categoryId: number,
    category: Category,
    prices: Price[]
}