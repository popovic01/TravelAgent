import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

import { NgxUiLoaderModule, NgxUiLoaderRouterModule, NgxUiLoaderHttpModule, NgxUiLoaderConfig, POSITION, SPINNER } from 'ngx-ui-loader';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OfferComponent } from './components/offer/offer.component';
import { OfferReviewComponent } from './components/offer-review/offer-review.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OfferUpsertComponent } from './components/offer-upsert/offer-upsert.component';
import { HttpInterceptorService } from './helpers/interceptor';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { NavbarComponent } from './components/navbar/navbar.component';

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
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,    
    ReactiveFormsModule,
    FormsModule,
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    NgxUiLoaderRouterModule.forRoot({ showForeground: false }),
    NgxUiLoaderHttpModule.forRoot({ showForeground: false }),
    NgMultiSelectDropDownModule.forRoot(),
    ToastrModule.forRoot()
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
