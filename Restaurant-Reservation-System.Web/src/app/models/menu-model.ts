export interface MenuModel {
    id: number
    restaurantId: number
    name: string
}

export interface DishModel {
    id: number
    name: string,
    price: number,
    isAvaiable: boolean,
    imageUrl: string|null
}

export interface CreateDishModel {
    name: string,
    price: number,
    isAvaiable: boolean,
}

export interface MenuDishModel {
    id: number
    name: string
    restaurantId: number
    dishes: DishModel[]
}