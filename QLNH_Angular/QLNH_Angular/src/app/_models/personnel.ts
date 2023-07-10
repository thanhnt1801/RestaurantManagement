import { Restaurant } from "./restaurant";
import { Role } from "./role";

export interface Personnel{
    id: number;
    userName: string;
    password: string;
    description: string;
    createdAt: Date;
    updatedAt: Date;
    offDuty: boolean;
    deleted: boolean;
    restaurant: Restaurant;
    role: Role;
    roleId: number;
}