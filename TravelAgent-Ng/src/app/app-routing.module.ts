import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OfferComponent } from './components/offer/offer.component';
import { OfferReviewComponent } from './components/offer-review/offer-review.component';
import { OfferUpsertComponent } from './components/offer-upsert/offer-upsert.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AdminGuard } from './services/guards/admin-guard.service';
import { LocationComponent } from './components/location/location.component';
import { TagComponent } from './components/tag/tag.component';
import { OfferTypeComponent } from './components/offer-type/offer-type.component';
import { TransportationTypeComponent } from './components/transportation-type/transportation-type.component';
import { UserGuard } from './services/guards/user-guard.service';

const routes: Routes = [
  // { path: '', pathMatch: 'full', redirectTo: 'home'  },
  { path: '', pathMatch: 'full', redirectTo: 'offers'  },

  { path: 'login', component: LoginComponent, pathMatch: 'full' },
  { path: 'register', component: RegistrationComponent, pathMatch: 'full' },

  { path: 'offers', component: OfferComponent, pathMatch: 'full' },
  { path: 'wishlist/:id', component: OfferComponent, pathMatch: 'full', canActivate: [UserGuard] },
  { path: 'offer-review/:id', component: OfferReviewComponent, pathMatch: 'full' },
  { path: 'offer-create', component: OfferUpsertComponent, pathMatch: 'full', canActivate: [AdminGuard] },
  { path: 'offer-edit/:id', component: OfferUpsertComponent, pathMatch: 'full', canActivate: [AdminGuard] },

  { path: 'locations', component: LocationComponent, pathMatch: 'full', canActivate: [AdminGuard] },
  { path: 'tags', component: TagComponent, pathMatch: 'full', canActivate: [AdminGuard] },
  { path: 'offer-types', component: OfferTypeComponent, pathMatch: 'full', canActivate: [AdminGuard] },
  { path: 'transportation-types', component: TransportationTypeComponent, pathMatch: 'full', canActivate: [AdminGuard] },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
