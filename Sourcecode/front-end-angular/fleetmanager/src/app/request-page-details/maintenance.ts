export interface IMaintenance{
    date : Date;
    price : number;
    garage : string;
}

export class Maintenance implements IMaintenance{
    date: Date;
    price: number;
    garage: string;

    constructor(date : Date, price : number, garage : string){
        this.date = date;
        this.price = price;
        this.garage = garage;
    }

}