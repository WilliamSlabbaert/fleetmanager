import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-request-page-details',
  templateUrl: './request-page-details.component.html',
  styleUrls: ['./request-page-details.component.css']
})
export class RequestPageDetailsComponent implements OnInit {

  _requestId! : number;
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this._requestId = +this.route.snapshot.paramMap.get('id')!;
  }

}
