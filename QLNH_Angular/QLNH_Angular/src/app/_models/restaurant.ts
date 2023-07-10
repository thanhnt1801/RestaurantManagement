export interface Restaurant {
  id: number;
  name: string;
  description: string;
  phone: string;
  address: string;
  created: Date;
  updated: Date;
  deleted: boolean;
  createdUser: object;
  updatedUser: object;
}

export class Restaurant implements Restaurant {
  id: number = 0;
  name = '';
  description = '';
  phone = '';
  address = '';
}