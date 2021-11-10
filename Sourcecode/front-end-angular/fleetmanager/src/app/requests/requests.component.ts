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
  private _requests : any[] = [];
  pageTitle: string = 'Requests';

  constructor(private _requestService: RequestService) {
  }
  ngOnInit(): void {
    this.setSettings();
  }
  private _requestObservable$: Observable<any> = this._requestService.getObservable;


  get getRequests(): IRequest[] {
    return this._requests;
  }
  onClickId(value: number) {
    this.idClick.emit(value);
  }
  setSettings(): void {
    this._requestObservable$.subscribe(
      (res) => {
        this._requests = res.body.returnValue.requests;
      },
      (err) => { console.log(err) }
    )
  }
}
