import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRequest } from '../models/request';

@Injectable({
  providedIn: 'root'
})
export class RequestPageService {

  private _chauffeurId!: number;
  private _vehicleId!: number;
  constructor(private http: HttpClient) { }

  postObservable(value: any): Observable<any> {
    return this.http.post(this.getURL(this._chauffeurId,this._vehicleId), value,{ observe: 'response' });
  }
  getURL(ch:number,vh:number): string {
    return "https://localhost:44388/Chauffeur/" + ch + "/Vehicle/" + vh + "/Requests";
  }
  set setChauffeurId(value: number){
    this._chauffeurId = value;
  }
  set setVehicleId(value: number){
    this._vehicleId = value;
  }

  get getVehicle(){
    return this._vehicleId;
  }
  get getChauffeur(){
    return this._chauffeurId;
  }
}
