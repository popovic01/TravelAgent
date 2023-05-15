export class Offer {
    id: number = 0;
    name: string = '';
    imagePath: string = '';
    description: string = '';
    departureLocation: string = '';
    startDate: string = '';
    endDate: string = '';
    price: number = 0.0;
    transportationType: string = '';
    offerType: string = '';
    locations: Array<string> = [];
    tags: Array<string> = [];
    availableSpots: number = 0;
}