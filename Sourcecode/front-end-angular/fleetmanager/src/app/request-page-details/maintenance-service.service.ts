import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MaintenanceServiceService {

  private _requestId!: number;
  constructor(private http: HttpClient) { }

  postObservable(value: any, id :number): Observable<any> {
    return this.http.post(this.getURL(id), value,{ observe: 'response' });
  }
  getURL(rq:number): string {
    return "https://localhost:44388/Request/" + rq + "/Maintenance/";
  }
  set setRequestId(value: number){
    this.setRequestId = value;
  }
  get getRequestId() : number {
    return this._requestId;
  }

  getURLInvoice(mn:number): string {
    return "https://localhost:44388/Maintenance/" + mn + "/Invoice/";
  }

  postObservableInvoice(value: any, id :number): Observable<any> {
    return this.http.post(this.getURLInvoice(id), value,{ observe: 'response' });
  }
}
