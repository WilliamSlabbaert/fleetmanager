import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { Observable } from "rxjs";
import { IChauffeurs } from "../models/chauffeurs";
import { ChauffeurService } from "../services/chauffeurs.service";

@Component({
    selector: 'pm-chauffeurs',
    templateUrl: './chauffeurs-list-component.html'
})
export class ChauffeursComponent implements OnInit {
    pageTitle: string = 'Chauffeurs';
    private _chauffeurs : any[] = [];
    constructor(private _chauffeurService: ChauffeurService) {
    }
    ngOnInit(): void {
        this.setSettings();
    }
    private _chauffeurObservable$: Observable<any> = this._chauffeurService.getObservable;
    @Output() idClick = new EventEmitter<number>()


    get getChauffeurs(): IChauffeurs[] {
        return this._chauffeurs;
    }
    onClickId(value: number) {
        this.idClick.emit(value);
    }
    setSettings(): void {
        this._chauffeurObservable$.subscribe(
            (res) => {
                this._chauffeurs = [res.body.returnValue];
            },
            (err) => { console.log(err) }
        )
    }

}