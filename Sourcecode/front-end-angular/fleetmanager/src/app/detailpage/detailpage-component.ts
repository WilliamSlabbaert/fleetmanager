import { Component, Input } from "@angular/core";

@Component({
    selector:"pm-detailPage",
    templateUrl:'./detailpage-component.html'
})

export class DetailPage{
    detailName : string = ' '
    @Input() detailObject : any = {};
}