import { Personnel } from "./personnel";
import { User } from "./user";

export interface Role{
    id: number,
    name: string,
    description: string,
    created: Date,
    updated: Date,
    deleted: boolean,
    user: Personnel[]
}