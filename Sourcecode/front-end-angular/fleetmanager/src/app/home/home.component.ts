import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  pageTitle: string = 'Fleetmanager';
  borderColor: string = "transparent";
  detailType: string = '';
  detailId!: number;
  private _pageType: number = 1;

  set setPage(value: number) {
    this._pageType = value;
  }
  get getPage(): number { return this._pageType; }
  pageClick(value: number) {
    this.setPage = value;
  }
  onEditClick(value: number, type: string) {
    this.detailType = type;
    this.detailId = value;
  }
}
