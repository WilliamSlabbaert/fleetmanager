import { Component, Output, EventEmitter, OnInit, OnDestroy } from "@angular/core";
import { Subscription } from "rxjs";
import { IVehicle } from "./vehicle";
import { VehicleService } from "./vehicle.service";
@Component({
    selector: 'pm-vehicles',
    templateUrl: './vehicles-list-component.html',
    styleUrls: ['./vehicles-list-component-style.css']
})


export class VehicleListComponent implements OnInit, OnDestroy {
    _pageTitle: string = 'Vehicles';
    _sub!: Subscription;
    private _vehicles: IVehicle[] = [];
    @Output() idClick = new EventEmitter<IVehicle>()
    constructor(private _vehicleService: VehicleService) { }


    ngOnInit(): void {
        this._sub = this._vehicleService.getVehiclesRequest();
    }

    get getPageNumber(): number {
        return this._vehicleService.getPageNumber;
    }

    get getPagingDetails(): string {
        
        if (this.getPageNumber == 1 && this.checkPageNumber("Next") == false) {
            return this.getVehicles.length.toString() + " of " + this._vehicleService.totalCount;
        }
        const formule = ((this.getPageNumber - 1) * 10) + this.getVehicles.length;
        return formule.toString()+ " of " + this._vehicleService.totalCount;;

    }

    get getVehicles(): IVehicle[] {
        this._vehicles = this._vehicleService.getVehicles;
        return this._vehicles;
    }
    checkPageNumber(value: string): boolean {
        if (value.toUpperCase() === "NEXT") {
            return this._vehicleService.getNextCheck;
        }
        return this._vehicleService.getPreviousCheck;
    }

    goToNextPage(): void {
        this._vehicleService.incrementPage();
        this.refreshVehicleSubscription();
    }
    goToPreviousPage(): void {
        this._vehicleService.decrementPage();
        this.refreshVehicleSubscription();
    }
    refreshVehicleSubscription() {
        this._sub = this._vehicleService.getVehiclesRequest();
        this._vehicles = this._vehicleService.getVehicles;
    }

    onClickId(value: number) {
        const vh = this.getVehicles.find(s => s.id == value)
        this.idClick.emit(vh);
    }

    ngOnDestroy(): void {
        this._sub.unsubscribe();
    }
}