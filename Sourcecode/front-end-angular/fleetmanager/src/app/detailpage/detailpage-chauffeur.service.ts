import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DetailpageChauffeurService {
  private _chauffeurId!: number;
  constructor(private http: HttpClient) { }

  get getURL(): string {
    return "https://localhost:44346/Chauffeur/" + this._chauffeurId;
  }
  getObservable(): Observable<any> {
    return this.http.get<Observable<any>>(this.getURL);
  }
  setChaffeurSettings(value: number) {
    this._chauffeurId = value;
  }
}
