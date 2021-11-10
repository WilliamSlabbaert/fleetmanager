import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { DetailpageService } from '../services/detailpage.service';

@Component({
  selector: 'pm-child-list',
  templateUrl: './child-list.component.html',
  styleUrls: ['./child-list.component.css']
})
export class ChildListComponent implements OnInit, OnChanges {
  @Input() detailName: string = '';
  @Input() detailId!: number;
  detailObject: any;
  private checkPage: boolean = false;
  constructor(private _detailPageService : DetailpageService) { }

  ngOnInit(): void { }

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
            console.log(val.returnValue)
        })
    }
}

  ngOnChanges() {
    if (this.detailName != '') {
      this.setSettings();
      this.setCheckPage = true;
    }
  }
}
