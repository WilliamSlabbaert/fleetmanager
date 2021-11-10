import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { IPaginationHeader } from 'src/shared/http-returnvalue/pageheader';
import { IFuelcards } from '../models/fuelcards';
import { GeneralChauffeurService } from './general-chauffeur.service';

@Injectable({
  providedIn: 'root'
})
export class FuelcardsService {
  constructor(private http: HttpClient,
    private _generalService : GeneralChauffeurService)  { }

  get getObservable(): Observable<any> {
    return this._generalService.getObservable;
  }

}
