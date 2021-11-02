import { Injectable } from "@angular/core";
import { IChauffeurs } from "./chauffeurs";


@Injectable({
    providedIn: 'root'
})
export class ChauffeurService{
    private _chauffeurs: IChauffeurs[] = [{
        "firstName" : "William",
        "id": 1,
        "lastName" : "Slabbaert",
        "city": "Lokeren",
        "dateOfBirth" : new Date,
        "nationalInsurenceNumber": "zeazeze",
        "isActive" : true,
        "street": "Poststraat",
        "houseNumber" : "59"
    },{
        "firstName" : "test",
        "id": 2,
        "lastName" : "test",
        "city": "rzere",
        "dateOfBirth" : new Date,
        "nationalInsurenceNumber": "zaezae",
        "isActive" : true,
        "street": "eazezaezae",
        "houseNumber" : "eazezae"
    },{
        "firstName" : "gzgze",
        "id": 3,
        "lastName" : "gze",
        "city": "rrrr",
        "dateOfBirth" : new Date,
        "nationalInsurenceNumber": "ezaezae",
        "isActive" : true,
        "street": "zzzzz",
        "houseNumber" : "ezaea"
    },];
    get getChauffeurs() : IChauffeurs[]{
        return this._chauffeurs;
    }
}