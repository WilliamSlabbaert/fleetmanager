import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Observable } from 'rxjs';
import { IChauffeurs } from '../models/chauffeurs';
import { GeneralChauffeurService } from '../services/general-chauffeur.service';
import { Request } from '../models/request-model';
import { RequestPageService } from '../services/request-page.service';
import { Router } from '@angular/router';

@Component({
  templateUrl: './request-page.component.html',
  styleUrls: ['./request-page.component.css']
})
export class RequestPageComponent implements OnInit {

  private startDateCheck: boolean = false;
  private endDateCheck: boolean = false;
  private statusCheck: boolean = false;
  private startDate!: Date;
  private endDate!: Date;
  private _chauffeurObservable$: Observable<any> = this._generalChauffeurService.getObservable;
  private _requestObservable$!: Observable<any>;
  private _chauffeur!: IChauffeurs
  private _requestType: number = 1;
  _requestId!: number;

  mystatus: string = "";
  carId!: number;


  constructor(
    private _generalChauffeurService: GeneralChauffeurService,
    private _requestPageService: RequestPageService,
    private router: Router) { }

  ngOnInit(): void {
    this.setSettings();
  }
  onInputStart(value: any) {
    this.startDate = value.target.value;
    const endDateInput = (<HTMLInputElement>document.querySelector('#endInput'));
    const statusInput = (<HTMLInputElement>document.querySelector('#statusInput'));
    if (!this.startDate) {
      this.startDateCheck = false;
      this.statusCheck = false;
    } else {
      this.startDateCheck = true;
    }
    statusInput.value = '';
    endDateInput.value = '';
  }
  onInputEnd(value: any) {
    this.endDate = value.target.value;
    const statusInput = (<HTMLInputElement>document.querySelector('#statusInput'));
    if (this.endDate < this.startDate) {
      this.endDateCheck = true;
      this.statusCheck = false;
    }
    else {
      this.endDateCheck = false;
      this.statusCheck = true;
    }
    statusInput.value = '';
  }

  get getStartDateCheck(): boolean {
    return this.startDateCheck;
  }
  get getEndDateCheck(): boolean {
    return this.endDateCheck;
  }
  get getStatusCheck(): boolean {
    return this.statusCheck;
  }
  setSettings(): void {
    this._chauffeurObservable$.subscribe(
      (res) => {
        this._chauffeur = res.body.returnValue;
      },
      (err) => { console.log(err) }
    )
  }
  get getChauffeur(): IChauffeurs {
    return this._chauffeur;
  }
  set setStatus(value: any) {
    this.mystatus = value.target.value;
  }
  radioBtn(value: any) {
    this._requestType = value.target.value == "Repairment" ? 2 : 1;
  }

  onNext(): void {
    const temp = new Request(
      this.startDate,
      this.endDate,
      this.mystatus,
      this._requestType
    );
    this.makePostRequest(temp);
  }
  makePostRequest(temp: any) {
    this._requestPageService.setVehicleId = this.carId;
    this._requestPageService.setChauffeurId = 1;
    this._requestObservable$ = this._requestPageService.postObservable(temp);

    this._requestObservable$.subscribe(data => {
      this._requestId = data.body.returnValue.id;
      this.router.navigateByUrl('/request/' + this._requestId);
    })
  }
  onCancel(){
    this.router.navigateByUrl('/home');
  }
}
