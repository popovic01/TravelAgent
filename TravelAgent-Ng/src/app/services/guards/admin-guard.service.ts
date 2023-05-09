import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }
  
  canActivate() {
    
    if (this.authService.isLoggedIn() && this.authService.isAdmin())
      return true;

    this.router.navigate(['login']);
    return false;  }
}
