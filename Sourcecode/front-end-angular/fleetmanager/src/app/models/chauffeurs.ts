import { IRequest } from "./request";
import { IChauffeurVehicles } from "./vehicle";

export interface IChauffeurs{
    id:number;
    firstName: string;
    lastName: string;
    city: string;
    street: string;
    houseNumber: string;
    dateOfBirth: Date;
    nationalInsurenceNumber: string;
    isActive: boolean;
    chauffeurVehicles: IChauffeurVehicles[];
    drivingLicenses: IDrivingLicense[];
    requests: IRequest[];
}
interface IDrivingLicense{
    id: number;
    type: number;
    chauffeurId: number;
}
interface IChauffeurFuelcards{

}
