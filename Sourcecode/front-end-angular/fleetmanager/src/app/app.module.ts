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
import { FuelcardsListComponent } from './fuelcards/fuelcards-list-component';
import { RequestsComponent } from './requests/requests.component';
import { ChildListComponent } from './child-list/child-list.component';
import { RouterModule } from '@angular/router';
import { RequestPageComponent } from './request-page/request-page.component';
import { HomeComponent } from './home/home.component';
import { CommonModule } from '@angular/common';
import { RequestPageDetailsComponent } from './request-page-details/request-page-details.component';

@NgModule({
  declarations: [
    AppComponent,
    VehicleListComponent,
    ConvertToSpacesPipe,
    ChauffeursComponent,
    DetailPage,
    FuelcardsListComponent,
    RequestsComponent,
    ChildListComponent,
    HomeComponent,
    RequestPageComponent,
    RequestPageDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    CommonModule,
    RouterModule.forRoot([
      {path: 'request', component: RequestPageComponent},
      {path: 'request/:id', component: RequestPageDetailsComponent},
      {path: 'home', component:HomeComponent},
      {path: '', redirectTo:'home',pathMatch:'full'},
      {path: '**', redirectTo:'home',pathMatch:'full'}

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
