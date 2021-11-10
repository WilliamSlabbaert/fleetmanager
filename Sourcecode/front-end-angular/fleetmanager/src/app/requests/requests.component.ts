import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { IRequest } from '../models/request';
import { RequestService } from '../services/request.service';

@Component({
  selector: 'pm-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {

  @Output() idClick = new EventEmitter<number>()

  pageTitle: string = 'Requests';

  constructor(private _requestService: RequestService) {
  }
  ngOnInit(): void {
    this.setSettings();
  }
  private _requestObservable$: Observable<any> = this._requestService.getObservable;


  get getRequests(): IRequest[] {
    return this._requestService.getRequests;
  }
  onClickId(value: number) {
    this.idClick.emit(value);
  }
  setSettings(): void {
    this._requestObservable$.subscribe(
      (res) => {
        this._requestService.setHeaders = JSON.parse(res.headers.get('X-Pagination'));
        this._requestService.setRequests = res.body.returnValue;
      },
      (err) => { console.log(err) }
    )
  }

  goToNextPage(): void {
    this._requestService.incrementPage();
    this.refreshChaffeurSubscription();
  }
  goToPreviousPage(): void {
    this._requestService.decrementPage();
    this.refreshChaffeurSubscription();
  }
  refreshChaffeurSubscription() {
    this._requestObservable$ = this._requestService.getObservable;
    this.setSettings();
  }

  getPageNumber(): number {
    return this._requestService.getPageNumber;
  }

  checkPageNumber(value: string): boolean {
    if (value.toUpperCase() === "NEXT") {
      return this._requestService.getNextCheck;
    }
    return this._requestService.getPageNumber == 1 ? false : true;
  }

  get getPagingDetails(): string {

    if (this.getPageNumber() == 1 && this.checkPageNumber("Next") == false) {
      return this.getRequests.length.toString() + " of " + this._requestService.totalCount;
    }
    const formule = ((this.getPageNumber() - 1) * 10) + this.getRequests.length;
    return formule.toString() + " of " + this._requestService.totalCount;;

  }

}
