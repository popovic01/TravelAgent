import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { NgxUiLoaderModule, NgxUiLoaderRouterModule, NgxUiLoaderHttpModule, NgxUiLoaderConfig, POSITION } from 'ngx-ui-loader';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OfferComponent } from './components/offer/offer.component';
import { OfferReviewComponent } from './components/offer-review/offer-review.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OfferUpsertComponent } from './components/offer-upsert/offer-upsert.component';
import { HttpInterceptorService } from './helpers/interceptor';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LocationComponent } from './components/location/location.component';
import { TagComponent } from './components/tag/tag.component';
import { OfferTypeComponent } from './components/offer-type/offer-type.component';
import { TransportationTypeComponent } from './components/transportation-type/transportation-type.component';
import { LocationDialogComponent } from './components/location-dialog/location-dialog.component';
import { TagDialogComponent } from './components/tag-dialog/tag-dialog.component';
import { OfferTypeDialogComponent } from './components/offer-type-dialog/offer-type-dialog.component';
import { TransportationTypeDialogComponent } from './components/transportation-type-dialog/transportation-type-dialog.component';

const loaderColor = '#d5b4b4';

const ngxUiLoaderConfig: NgxUiLoaderConfig = {
  bgsPosition: POSITION.centerCenter,
  bgsColor: loaderColor,
  fgsColor: loaderColor,
  bgsSize: 70,
  fgsSize: 70
}

@NgModule({
  declarations: [
    AppComponent,
    OfferComponent,
    OfferReviewComponent,
    OfferUpsertComponent,
    LoginComponent,
    RegistrationComponent,
    NavbarComponent,
    LocationComponent,
    TagComponent,
    OfferTypeComponent,
    TransportationTypeComponent,
    LocationDialogComponent,
    TagDialogComponent,
    OfferTypeDialogComponent,
    TransportationTypeDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,    
    ReactiveFormsModule,
    FormsModule,
    MatToolbarModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    NgxUiLoaderRouterModule.forRoot({ showForeground: false }),
    NgxUiLoaderHttpModule.forRoot({ showForeground: false }),
    NgMultiSelectDropDownModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
