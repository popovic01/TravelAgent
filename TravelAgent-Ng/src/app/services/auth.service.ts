import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private jwtService = new JwtHelperService();

  constructor(private http: HttpClient, private router: Router) { }

  register(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}user/register`, obj);
  }

  login(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}user/login`, obj);
  }

  logout() {
    localStorage.removeItem('token');
    window.sessionStorage.clear(); //proveriti da li ovo treba
    this.router.navigate(['']);
  }

  isLoggedIn() {
    let token = localStorage.getItem('token');

    if (!token)
      return false;

    return !this.jwtService.isTokenExpired(token);
  }

  public getCurrentUser() {
    let token = localStorage.getItem('token');

    if (!token)
      return null;

    return this.jwtService.decodeToken(token);
  }
}
