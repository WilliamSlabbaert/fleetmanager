import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { IFuelcards } from '../models/fuelcards';
import { FuelcardsService } from '../services/fuelcards.service';

@Component({
  selector: 'pm-fuelcards',
  templateUrl: './fuelcards-list-component.html',
  styleUrls: ['./fuelcards-list-component.css']
})
export class FuelcardsListComponent implements OnInit {
  @Output() idClick = new EventEmitter<number>()
  pageTitle: string = "Fuelcards"
  private _fuelcardsObservable$: Observable<any> = this._fuelcardService.getObservable;
  constructor(private _fuelcardService: FuelcardsService) { }

  ngOnInit(): void {
    this.setSettings();
  }
  get getFuelcards(): IFuelcards[] {
    return this._fuelcardService.getFuelcards;
  }
  onClickId(value: number) {
    this.idClick.emit(value);
  }
  setSettings(): void {
    this._fuelcardsObservable$.subscribe(
      (res) => {
        this._fuelcardService.setHeaders = JSON.parse(res.headers.get('X-Pagination'));
        this._fuelcardService.setFuelcards = res.body.returnValue;
      },
      (err) => { console.log(err) }
    )
  }
  getPageNumber(): number {
    return this._fuelcardService.getPageNumber;
  }
  checkPageNumber(value: string): boolean {
    if (value.toUpperCase() === "NEXT") {
      return this._fuelcardService.getNextCheck;
    }
    return this._fuelcardService.getPageNumber == 1 ? false : true;
  }

  get getPagingDetails(): string {

    if (this.getPageNumber() == 1 && this.checkPageNumber("Next") == false) {
      return this.getFuelcards.length.toString() + " of " + this._fuelcardService.totalCount;
    }
    const formule = ((this.getPageNumber() - 1) * 10) + this.getFuelcards.length;
    return formule.toString() + " of " + this._fuelcardService.totalCount;;

  }
  goToNextPage(): void {
    this._fuelcardService.incrementPage();
    this.refreshChaffeurSubscription();
  }
  goToPreviousPage(): void {
    this._fuelcardService.decrementPage();
    this.refreshChaffeurSubscription();
  }
  refreshChaffeurSubscription() {
    this._fuelcardsObservable$ = this._fuelcardService.getObservable;
    this.setSettings();
  }
  
}
