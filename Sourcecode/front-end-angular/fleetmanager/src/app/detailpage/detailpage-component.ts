import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";
import { Observable, Subscription } from "rxjs";
import { IVehicle } from "../vehicles/vehicle";
import { DetailpageServiceVehicle } from "./detailpage-vehicle.service";

@Component({
    selector: "pm-detailPage",
    templateUrl: './detailpage-component.html'
})

export class DetailPage implements OnChanges {
    @Input() detailName: string = '';
    @Input() detailId!: number;
    detailObject: any;
    private checkPage: boolean = false;
    constructor(private _detailPageVehicleService: DetailpageServiceVehicle) { }

    get getCheckPage(): boolean {
        return this.checkPage;
    }
    set setCheckPage(value: boolean) {
        this.checkPage = value;
    }

    setSettings() {
        this._detailPageVehicleService.setVehicleSettings(this.detailId);
        const observable = this._detailPageVehicleService.getObservable();
        observable.subscribe(val =>{
            this.detailObject = val.returnValue;
        })
    }

    ngOnChanges(changes: SimpleChanges) {
        if (this.detailName != '') {
            this.setSettings();
            this.setCheckPage = true;
        }
    }
}