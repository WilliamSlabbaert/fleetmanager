import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subscription, throwError } from "rxjs";
import { IVehicle } from "../models/vehicle";
import { IPaginationHeader } from "src/shared/http-returnvalue/pageheader";
import { GeneralChauffeurService } from "./general-chauffeur.service";

@Injectable({
    providedIn: 'root'
})
export class VehicleService {
    private _chauffeurObservable$: Observable<any> = this._generalService.getObservable;
    private _vehicles: IVehicle[] = [];
    constructor(private http: HttpClient,
        private _generalService: GeneralChauffeurService) { }

    setVehicles(): void{
        this._chauffeurObservable$.subscribe(
            (val) =>{
                this._vehicles = val.body.returnValue.chauffeurVehicles.map((item : any) => item['vehicle']);
                console.log(this._vehicles)
            },
            (err) => {console.log(err)}
        )
    }
    
    get getVehicles(): IVehicle[] {
        return this._vehicles;
    }
}