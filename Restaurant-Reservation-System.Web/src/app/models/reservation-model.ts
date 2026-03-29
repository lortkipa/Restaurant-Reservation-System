export interface ReservationModel {
    id: number
    customerId: number
    restaurantId: number
    statusId: number
    date: Date
    tableNumber: number
    guestCount: number
}
