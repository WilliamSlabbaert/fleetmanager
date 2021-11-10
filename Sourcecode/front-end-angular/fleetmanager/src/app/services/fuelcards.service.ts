import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { IPaginationHeader } from 'src/shared/http-returnvalue/pageheader';
import { IFuelcards } from '../models/fuelcards';

@Injectable({
  providedIn: 'root'
})
export class FuelcardsService {
  constructor(private http: HttpClient) { }
  private _pageNumber: number = 1
  private _headers!: IPaginationHeader;
  private _fuelcards!: IFuelcards[];
  get getURL(): string {
    return "https://localhost:44346/Fuelcard?PageNumber=" + this._pageNumber + "&PageSize=10";
  }
  get getObservable(): Observable<any> {
    return this.http.get<Observable<any>>(this.getURL, { observe: 'response' });
  }
  get totalCount(): number {
    if (this._headers) {
      return this._headers.TotalCount;
    }
    return 0;
  }
  
  get getFuelcards(): IFuelcards[] {
    return this._fuelcards;
  }
  get getPageNumber(): number {
    return this._pageNumber;
  }
  get getHeaders(): IPaginationHeader {
    return this._headers;
  }
  set setFuelcards(value: IFuelcards[]) {
    this._fuelcards = value;
  }
  set setHeaders(value: IPaginationHeader) {
    this._headers = value;
  }
  get getNextCheck(): boolean {
    if (this._headers) {
      return this._headers.HasNext;
    }
    return false;
  }
  incrementPage(): void {
    this._pageNumber = this._pageNumber + 1;
  }
  decrementPage(): void {
    if (this._headers.HasPrevious) {
      this._pageNumber = this._pageNumber - 1;
    }
  }
}
