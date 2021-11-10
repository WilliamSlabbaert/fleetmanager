import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subscription, throwError } from "rxjs";
import { IVehicle } from "../models/vehicle";
import { IPaginationHeader } from "src/shared/http-returnvalue/pageheader";

@Injectable({
    providedIn: 'root'
})
export class VehicleService {
    private _pageNumber: number = 1
    private _vehicleObservable$: Observable<any> = this.getObservable;
    private _vehicles: IVehicle[] = [];
    private _headers!: IPaginationHeader;
    constructor(private http: HttpClient) { }

    getVehiclesRequest(): Subscription {
        return this._vehicleObservable$.subscribe({
            next: (item) => {
                this._headers = JSON.parse(item.headers.get('X-Pagination'));
                const items: IVehicle[] = item.body.returnValue;
                items.forEach(element => {
                    element.buildDate = new Date(element.buildDate);
                });
                this._vehicles = items
            },
            error: err => console.log(err)
        })
    }

    get getPageNumber(): number {
        return this._pageNumber;
    }
    get totalCount(): number {
        if (this._headers) {
            return this._headers.TotalCount;
        }
        return 0;
    }
    incrementPage(): void {
        this._pageNumber = this._pageNumber + 1;
        this.setObservable = this.getObservable;
    }
    decrementPage(): void {
        if (this._headers.HasPrevious) {
            this._pageNumber = this._pageNumber - 1;
            this.setObservable = this.getObservable;
        }
    }
    get getHeaders(): IPaginationHeader {
        return this._headers;
    }
    get getPreviousCheck(): boolean {
        if (this._headers) {
            return this._headers.HasPrevious;
        }
        return false;
    }
    get getNextCheck(): boolean {
        if (this._headers) {
            return this._headers.HasNext;
        }
        return false;
    }

    get getURL(): string {
        return "https://localhost:44346/Vehicle?PageNumber=" + this._pageNumber + "&PageSize=10";
    }

    get getObservable(): Observable<any> {
        return this.http.get<Observable<any>>(this.getURL, { observe: 'response' });
    }
    set setObservable(value: Observable<any>) {
        this._vehicleObservable$ = value;
    }

    get getVehicles(): IVehicle[] {
        return this._vehicles;
    }
}