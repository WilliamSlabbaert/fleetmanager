import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPaginationHeader } from 'src/shared/http-returnvalue/pageheader';
import { IRequest } from './request';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private _requests!: IRequest[];
  private _pageNumber: number = 1
  private _headers!: IPaginationHeader;
  constructor(private http: HttpClient) { }
  get getURL(): string {
    return "https://localhost:44346/Request?PageNumber=" + this._pageNumber + "&PageSize=10";
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
  get getRequests(): IRequest[] {
    return this._requests;
  }
  get getPageNumber(): number {
    return this._pageNumber;
  }
  get getHeaders(): IPaginationHeader {
    return this._headers;
  }
  set setRequests(value: IRequest[]) {
    this._requests = value;
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
