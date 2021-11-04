import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DetailpageService {
  private _Id!: number;
  constructor(private http: HttpClient) { }

  getURL(value: string): string {
    return "https://localhost:44346/" + value + "/" + this._Id;
  }
  getObservable(value: string): Observable<any> {
    return this.http.get<Observable<any>>(this.getURL(value));
  }
  setSettings(value: number) {
    this._Id = value;
  }
}

