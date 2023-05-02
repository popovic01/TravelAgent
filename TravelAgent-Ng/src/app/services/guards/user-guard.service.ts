import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }
  
  canActivate() {
    
    if (this.authService.isLoggedIn() && this.authService.getCurrentUser()?.role == 'client')
      return true;

    this.router.navigate(['']);
    return false;  }
}
