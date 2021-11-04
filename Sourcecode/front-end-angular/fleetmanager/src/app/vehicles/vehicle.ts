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
    chauffeurId : number;
    isActive : boolean;
}