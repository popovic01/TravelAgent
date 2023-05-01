import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OfferComponent } from './components/offer/offer.component';
import { OfferReviewComponent } from './components/offer-review/offer-review.component';
import { OfferUpsertComponent } from './components/offer-upsert/offer-upsert.component';

const routes: Routes = [
  // { path: '', pathMatch: 'full', redirectTo: 'login'  },
  { path: '', pathMatch: 'full', redirectTo: 'offers'  },
  { path: 'offers', component: OfferComponent, pathMatch: 'full' },
  { path: 'offer-review/:id', component: OfferReviewComponent, pathMatch: 'full' },
  { path: 'offer-create', component: OfferUpsertComponent, pathMatch: 'full' },
  { path: 'offer-edit/:id', component: OfferUpsertComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
