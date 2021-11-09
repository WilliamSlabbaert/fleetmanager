import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { observable, Observable } from 'rxjs';
import { IChauffeurs } from '../chauffeurs/chauffeurs';
import { GeneralChauffeurService } from '../general-chauffeur.service';
import { Invoice } from './invoice';
import { Maintenance } from './maintenance';
import { MaintenanceServiceService } from './maintenance-service.service';

@Component({
  selector: 'app-request-page-details',
  templateUrl: './request-page-details.component.html',
  styleUrls: ['./request-page-details.component.css']
})
export class RequestPageDetailsComponent implements OnInit {

  private _chauffeurObservable$: Observable<any> = this._generalChauffeurService.getObservable;
  private _requestId!: number;
  private _chauffeur!: IChauffeurs;
  private _maintenanceObservable$!: Observable<any>;
  type!: string;
  maintenanceDate!: Date;
  maintenancePrice!: number;
  maintenanceGarage!: string;
  maintenanceImage!: any;
  checkPage: boolean = false;

  constructor(private route: ActivatedRoute,
    private _generalChauffeurService: GeneralChauffeurService,
    private _maintenanceService: MaintenanceServiceService,
    private router: Router) { }


  ngOnInit(): void {
    this._requestId = +this.route.snapshot.paramMap.get('id')!;
    this.setSettings();
  }

  setSettings(): void {
    this._chauffeurObservable$.subscribe(
      (res) => {
        this._chauffeur = res.body.returnValue;
        const temp = this._chauffeur.requests.find(s => s.id === this._requestId);
        if (temp !== undefined) {
          this.checkPage = true;
          this.type = temp.type == 1 ? "Maintenance" : "Repairment";
        }
        console.log(this._chauffeur);
      },
      (err) => { console.log(err) }
    )
  }
  checkMaintenance(): boolean {
    if (this.maintenanceDate === null || this.maintenanceDate === undefined || !this.maintenanceDate) {
      return false;
    }
    if (this.maintenanceGarage === null || this.maintenanceGarage.trim() === '' || this.maintenanceGarage === undefined) {
      return false;
    }
    if (this.maintenancePrice === null || this.maintenancePrice === undefined) {
      return false;
    }
    return true;
  }

  toBase64 = (file: any) => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
  });
  async test(value: any) {
    this.maintenanceImage = (await this.toBase64(value.target.files[0]))
  }
  onSubmit() {
    this.postMaintenance()
  }
  postMaintenance() {
    const temp = new Maintenance(
      this.maintenanceDate,
      this.maintenancePrice,
      this.maintenanceGarage)
    this._maintenanceObservable$ = this._maintenanceService.postObservable(temp, this._requestId);

    this._maintenanceObservable$.subscribe(data => {
      const temp = data.body.returnValue.id;
      const tempValue = new Invoice(this.maintenanceImage);
      this._maintenanceService.postObservableInvoice(tempValue,temp).subscribe(val =>{
        console.log(val);
        this.router.navigateByUrl('/home');
      })
    })
  }
}
