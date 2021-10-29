import { Component, Input, OnChanges } from "@angular/core";

@Component({
    selector:"pm-detailPage",
    templateUrl:'./detailpage-component.html'
})

export class DetailPage implements OnChanges{
    @Input() detailName : string = '';
    @Input() detailObject : any = null;
    private checkPage : boolean = false;
    get getCheckPage() : boolean{
        return this.checkPage;
    }
    set setCheckPage(value :boolean){
        this.checkPage = value;
    }

    ngOnChanges(){
        if(this.detailName != ''){
            this.setCheckPage = true;
        }
    }
    changeDateTime(value : Date) : any{
        
    }
}