import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";
import { Observable, Subscription } from "rxjs";
import { IVehicle } from "../vehicles/vehicle";
import { DetailpageService } from "./detailpage.service";

@Component({
    selector: "pm-detailPage",
    templateUrl: './detailpage-component.html'
})

export class DetailPage implements OnChanges {
    @Input() detailName: string = '';
    @Input() detailId!: number;
    detailObject: any;
    private checkPage: boolean = false;
    constructor(private _detailPageService: DetailpageService) { }

    get getCheckPage(): boolean {
        return this.checkPage;
    }
    set setCheckPage(value: boolean) {
        this.checkPage = value;
    }

    setSettings() {
        if (this.detailName === "VEHICLE") {
            this._detailPageService.setSettings(this.detailId);
            this._detailPageService.getObservable("Vehicle").subscribe(val => {
                this.detailObject = val.returnValue;
            })
        }
        else if (this.detailName === "CHAUFFEUR") {
            this._detailPageService.setSettings(this.detailId);
            this._detailPageService.getObservable("Chauffeur").subscribe(val => {
                this.detailObject = val.returnValue;
            })
        }
        else if (this.detailName === "FUELCARD") {
            this._detailPageService.setSettings(this.detailId);
            this._detailPageService.getObservable("Fuelcard").subscribe(val => {
                this.detailObject = val.returnValue;
            })
        }
        else if (this.detailName === "REQUEST") {
            this._detailPageService.setSettings(this.detailId);
            this._detailPageService.getObservable("Request").subscribe(val => {
                this.detailObject = val.returnValue;
            })
        }
    }

    ngOnChanges(changes: SimpleChanges) {
        if (this.detailName != '') {
            this.setSettings();
            this.setCheckPage = true;
        }
    }
}