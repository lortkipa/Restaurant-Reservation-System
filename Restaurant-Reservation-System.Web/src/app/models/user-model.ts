export interface PersonModel {
    firstName: string;
    lastName: string;
    phone: string;
    address: string;
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