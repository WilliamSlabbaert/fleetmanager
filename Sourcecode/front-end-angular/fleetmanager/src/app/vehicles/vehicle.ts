export interface IVehicle{
    id: number;
    chassis: string;
    brand: string;
    model: string;
    buildDate: Date;
    fuelType: string;
    type: string;
    chauffeurVehicles: IChauffeurVehicles[];
}

export interface IChauffeurVehicles{
    vehicleId : number;
    vehicle: any;
    chauffeurId : number;
    chauffeur : any;
    isActive : boolean;
}