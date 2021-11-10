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
  private _fuelcards : any[] = [];
  private _fuelcardsObservable$: Observable<any> = this._fuelcardService.getObservable;
  constructor(private _fuelcardService: FuelcardsService) { }

  ngOnInit(): void {
    this.setSettings();
  }
  get getFuelcards(): IFuelcards[] {
    return this._fuelcards;
  }
  onClickId(value: number) {
    this.idClick.emit(value);
  }
  setSettings(): void {
    this._fuelcardsObservable$.subscribe(
      (res) => {
        
        this._fuelcards = res.body.returnValue.chauffeurFuelCards.map((item : any) => item['fuelcard']);
      },
      (err) => { console.log(err) }
    )
  }
  
}
