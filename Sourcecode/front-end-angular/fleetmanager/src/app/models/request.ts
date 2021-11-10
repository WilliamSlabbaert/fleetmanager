export interface IRequest{
    id: number;
    startDate : Date;
    endDate: Date;
    status: string;
    type:number;
    vehicleId:number;
    chauffeurId: number;
}