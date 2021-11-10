import { Component, Output, EventEmitter, OnInit, OnDestroy } from "@angular/core";
import { Subscription } from "rxjs";
import { IPaginationHeader } from "src/shared/http-returnvalue/pageheader";
import { IVehicle } from "../models/vehicle";
import { VehicleService } from "../services/vehicle.service";
@Component({
    selector: 'pm-vehicles',
    templateUrl: './vehicles-list-component.html',
    styleUrls: ['./vehicles-list-component-style.css']
})


export class VehicleListComponent implements OnInit {
    _pageTitle: string = 'Vehicles';
    private _vehicles: IVehicle[] = [];
    @Output() idClick = new EventEmitter<number>()
    constructor(private _vehicleService: VehicleService) { }


    ngOnInit(): void {
        this._vehicleService.setVehicles();
    }

    get getVehicles(): IVehicle[] {
        this._vehicles = this._vehicleService.getVehicles;
        return  this._vehicles;
    }

    onClickId(value: number) {
        this.idClick.emit(value);
    }
}