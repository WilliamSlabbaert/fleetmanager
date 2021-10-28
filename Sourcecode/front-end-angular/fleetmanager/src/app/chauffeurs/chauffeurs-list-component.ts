import { Component } from "@angular/core";
import { IChauffeurs } from "./chauffeurs";

@Component({
    selector: 'pm-chauffeurs',
    templateUrl: './chauffeurs-list-component.html'
})
export class ChauffeursComponent{
    pageTitle :string = 'Chauffeurs';

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