import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IPaginationHeader } from "src/shared/http-returnvalue/pageheader";
import { IChauffeurs } from "../models/chauffeurs";
import { GeneralChauffeurService } from "./general-chauffeur.service";


@Injectable({
    providedIn: 'root'
})
export class ChauffeurService{
    constructor(private http: HttpClient,
        private _generalService : GeneralChauffeurService) { }

    get getObservable(): Observable<any> {
        return this._generalService.getObservable;
    }
}