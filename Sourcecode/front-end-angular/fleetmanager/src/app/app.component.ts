import { Component } from "@angular/core";

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  template: `
  <div class='card wholePage'>
    <div class='card'
        [style.margin.px]='50'
        [style.borderRadius.px] ='20'
        [style.borderColor]='borderColor'>
        <div 
        [style.margin.px]='50'>
          <button [style.marginRight.px]= '10' 
          class="btn btn-dark" 
          (click)='pageClick(1)'>Vehicles</button>
          <button class="btn btn-dark" (click)='pageClick(2)'>Chauffeurs</button>
        </div>
        <div>
          <pm-vehicles (idClick)='onEditClick($event,"vehicle")'  *ngIf='getPage == 1'></pm-vehicles>
          <pm-chauffeurs *ngIf='getPage == 2'></pm-chauffeurs>
        </div>
       
    </div>
    <div class="container-fluid detailWrapper">
      <div class="card detailPage"
        [style.borderRadius.px] ='20'
        [style.borderColor]='borderColor'>
            <pm-detailPage></pm-detailPage>
            <h1>test</h1>
            <h1>test</h1>
            <h1>test</h1>
        </div>
        <div class="card dashBoardPage"
        [style.borderRadius.px] ='20'
        [style.borderColor]='borderColor'>
            <h1>test</h1>
            <h1>test</h1>
            <h1>test</h1>
        </div>
    </div>
  </div>`
})

export class AppComponent {
  pageTitle: string = 'Fleetmanager';
  borderColor: string = "transparent";
  detailType : string = '';
  private _pageType: number = 1;

  set setPage(value: number) {
    this._pageType = value;
  }
  get getPage(): number { return this._pageType; }
  pageClick(value: number) {
    this.setPage = value;
  }
  onEditClick(value:any, type:string){
      this.detailType = type;
      console.log(value)
  }

}