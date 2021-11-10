import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GeneralChauffeurService {
  constructor(private http: HttpClient) { }

  get getURL(): string {
    return "https://localhost:44346/Chauffeur/1";
  }
  get getObservable(): Observable<any> {
    return this.http.get<Observable<any>>(this.getURL, { observe: 'response' });
  }
}
