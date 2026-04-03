export interface RoleModel {
    id: number;
    name: string;
}

export enum Roles {
    Admin = 1,
    Worker,
    Customer
}