import { Component, Output ,EventEmitter} from "@angular/core";
import { IVehicle } from "./vehicle";
@Component({
    selector:'pm-vehicles',
    templateUrl: './vehicles-list-component.html',
    styleUrls:['./vehicles-list-component-style.css']
})


export class VehicleListComponent{
    pageTitle :string = 'Vehicles';
    @Output() idClick = new EventEmitter<IVehicle>()

    private _vehicles: IVehicle[] = [{
        "chassis" : "product 1",
        "id": 1,
        "brand" : "BMW",
        "model": "3-serie",
        "buildDate" : new Date,
        "fuelType": "diesel",
        "type" : "1"
    },{
        "chassis" : "product 2",
        "id": 2,
        "brand" : "AUDI",
        "model": "A-3",
        "buildDate" : new Date,
        "fuelType": "gas",
        "type" : "2",
    },{
        "chassis" : "product 3",
        "id": 3,
        "brand" : "SEAT",
        "model": "LEON-219",
        "buildDate" : new Date,
        "fuelType": "hybrid",
        "type" : "3"
    }];
    private _vehiclesFiltered :IVehicle[] = this._vehicles;

    get getVehicles(): IVehicle[]{
        return this._vehicles;
    }
    get getFilteredVehicles(): IVehicle[]{
        return this._vehiclesFiltered;
    }
    set setFilteredVehicles(value : IVehicle[]){
        this._vehiclesFiltered = value;
    }
    logTest(value:any) : void {
        console.log(value)
    }
    
    changeFilter(input:any) : void{
        const filter : string = input.value;
        const temp : IVehicle[] = this.getVehicles.filter((vh: IVehicle)=>{
            return vh.model.toLocaleLowerCase().includes(filter.toLocaleLowerCase());
        })
        this.setFilteredVehicles = temp;
    }
    onClickId(value:number){
        const vh = this.getVehicles.find(s => s.id == value)
        this.idClick.emit(vh);
    }
}