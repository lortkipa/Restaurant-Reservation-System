export interface PersonModel {
    id: number;
    firstName: string;
    lastName: string;
    phone: string;
    address: string;
}

export interface UserModel {
    id: number;
    username: string;
    email: string;
    registrationDate: Date;
}

export interface UserPersonModel {
    user: UserModel;
    person : PersonModel;
}

export interface RegisterModel {
    person: PersonModel;
    username: string;
    email: string;
    password: string;
    registrationDate: Date;
}

export interface LoginModel {
    username: string;
    password: string;
}