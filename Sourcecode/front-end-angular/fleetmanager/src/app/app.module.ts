import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VehicleListComponent } from './vehicles/vehicles-list-component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http'
import { ConvertToSpacesPipe } from 'src/shared/convert-to-spaces';
import { ChauffeursComponent } from './chauffeurs/chauffeurs-list-component';
import { DetailPage } from './detailpage/detailpage-component';
import { FuelcardsListComponent } from './fuelcards/fuelcards-list/fuelcards-list-component';

@NgModule({
  declarations: [
    AppComponent,
    VehicleListComponent,
    ConvertToSpacesPipe,
    ChauffeursComponent,
    DetailPage,
    FuelcardsListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
