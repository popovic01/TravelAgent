import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { OfferComponent } from '../offer/offer.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  providers: [OfferComponent]
})
export class NavbarComponent implements OnInit {

  userId: number = 0;

  constructor(public authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    if (this.authService.getCurrentUser()?.UserId)
      this.userId = Number(this.authService.getCurrentUser().UserId);
  }

  wishlist() {
    this.router.navigate(['/wishlist', Number(this.authService.getCurrentUser().UserId)]);
  }

  logout() {
    this.authService.logout();
  }

}
