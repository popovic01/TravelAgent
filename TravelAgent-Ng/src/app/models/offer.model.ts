export class Offer {
    id: number = 0;
    name: string = '';
    description: string = '';
    departureLocation: string = '';
    startDate: Date = new Date();
    endDate: Date = new Date();
    price: number = 0.0;
    transportationType: string = '';
    offerType: string = '';
    locations: Array<string> = [];
    tags: Array<string> = [];
    availableSpots: number = 0;
}