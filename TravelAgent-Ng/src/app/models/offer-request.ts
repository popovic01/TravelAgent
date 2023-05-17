export class OfferRequest {
    id: number = 0;
    departureLocation: string = '';
    startDate: string = '';
    endDate: string = '';
    maxPrice: number = 0.0;
    transportationTypeId: number = 0;
    locationIds: Array<number> = [];
    spotNumber: number = 0;
    clientId: number = 0;
}