import { HttpClient } from '@angular/common/http';
import { Injectable, OnChanges, SimpleChanges } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { IVehicle } from '../vehicles/vehicle';

@Injectable({
  providedIn: 'root'
})
export class DetailpageServiceVehicle {
  private _vehicleId!: number;
  constructor(private http: HttpClient) { }

  get getURL(): string {
    return "https://localhost:44346/Vehicle/" + this._vehicleId;
  }
  getObservable(): Observable<any> {
    return this.http.get<Observable<any>>(this.getURL);
  }
  setVehicleSettings(value: number) {
    this._vehicleId = value;
  }
}
