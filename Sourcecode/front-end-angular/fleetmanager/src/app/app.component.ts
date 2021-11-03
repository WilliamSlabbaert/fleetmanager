import { Component } from "@angular/core";
import { IVehicle } from "./vehicles/vehicle";

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html'
})

export class AppComponent {
  pageTitle: string = 'Fleetmanager';
  borderColor: string = "transparent";
  detailType : string = '';
  detailId! : number;
  private _pageType: number = 1;

  set setPage(value: number) {
    this._pageType = value;
  }
  get getPage(): number { return this._pageType; }
  pageClick(value: number) {
    this.setPage = value;
  }
  onEditClick(value:number, type:string){
      this.detailType = type;
      this.detailId = value;
  }

}