import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { Observable } from "rxjs";
import { IChauffeurs } from "./chauffeurs";
import { ChauffeurService } from "./chauffeurs.service";

@Component({
    selector: 'pm-chauffeurs',
    templateUrl: './chauffeurs-list-component.html'
})
export class ChauffeursComponent implements OnInit {
    pageTitle: string = 'Chauffeurs';
    constructor(private _chauffeurService: ChauffeurService) {
    }
    ngOnInit(): void {
        this.setSettings();
    }
    private _chauffeurObservable$: Observable<any> = this._chauffeurService.getObservable;
    @Output() idClick = new EventEmitter<number>()


    get getChauffeurs(): IChauffeurs[] {
        return this._chauffeurService.getChauffeurs;
    }
    onClickId(value: number) {
        this.idClick.emit(value);
    }
    setSettings(): void {
        this._chauffeurObservable$.subscribe(
            (res) => {
                this._chauffeurService.setHeaders = JSON.parse(res.headers.get('X-Pagination'));
                this._chauffeurService.setChauffeurs = res.body.returnValue;
            },
            (err) => { console.log(err) }
        )
    }

    goToNextPage(): void {
        this._chauffeurService.incrementPage();
        this.refreshChaffeurSubscription();
    }
    goToPreviousPage(): void {
        this._chauffeurService.decrementPage();
        this.refreshChaffeurSubscription();
    }
    refreshChaffeurSubscription() {
        this._chauffeurObservable$ = this._chauffeurService.getObservable;
        this.setSettings();
    }

    getPageNumber(): number {
        return this._chauffeurService.getPageNumber;
    }

    checkPageNumber(value: string): boolean {
        if (value.toUpperCase() === "NEXT") {
            return this._chauffeurService.getNextCheck;
        }
        return this._chauffeurService.getPageNumber == 1 ? false : true;
    }

    get getPagingDetails(): string {
        
        if (this.getPageNumber() == 1 && this.checkPageNumber("Next") == false) {
            return this.getChauffeurs.length.toString() + " of " + this._chauffeurService.totalCount;
        }
        const formule = ((this.getPageNumber() - 1) * 10) + this.getChauffeurs.length;
        return formule.toString()+ " of " + this._chauffeurService.totalCount;;

    }
}