export class OfferRequest {
    id: number = 0;
    departureLocation: string = '';
    description: string = '';
    startDate: string = '';
    endDate: string = '';
    maxPrice: number = 0.0;
    transportationType: string = '';
    locations: Array<string> = [];
    spotNumber: number = 0;
    clientId: number = 0;
}