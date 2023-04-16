import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OfferComponent } from './components/offer/offer.component';
import { OfferReviewComponent } from './components/offer-review/offer-review.component';

const routes: Routes = [
  { path: 'offers', component: OfferComponent, pathMatch: 'full' },
  { path: 'offer-review/:id', component: OfferReviewComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
