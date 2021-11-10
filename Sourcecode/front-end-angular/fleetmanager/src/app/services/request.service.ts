import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPaginationHeader } from 'src/shared/http-returnvalue/pageheader';
import { IRequest } from '../models/request';
import { GeneralChauffeurService } from './general-chauffeur.service';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  constructor(private http: HttpClient,
    private _generalService : GeneralChauffeurService) { }

  get getObservable(): Observable<any> {
    return this._generalService.getObservable;
  }
}
