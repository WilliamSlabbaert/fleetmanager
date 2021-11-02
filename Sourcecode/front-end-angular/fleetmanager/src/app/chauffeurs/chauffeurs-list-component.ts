import { Component, EventEmitter, Output } from "@angular/core";
import { IChauffeurs } from "./chauffeurs";
import { ChauffeurService } from "./chauffeurs.service";

@Component({
    selector: 'pm-chauffeurs',
    templateUrl: './chauffeurs-list-component.html'
})
export class ChauffeursComponent{
    pageTitle :string = 'Chauffeurs';
    @Output() idClick = new EventEmitter<IChauffeurs>()
    private _chauffeurService;
    constructor(chauffeurService: ChauffeurService){
        this._chauffeurService = chauffeurService;
    }

    get getChauffeurs() : IChauffeurs[]{
        return this._chauffeurService.getChauffeurs;
    }
    onClickId(value:number){
        const vh = this.getChauffeurs.find(s => s.id == value)
        this.idClick.emit(vh);
    }
}