import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OfferService {

  constructor(private http: HttpClient) { }

  edit(obj: any, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}offer/${id}`, obj);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}offer/${id}`);
  }

  add(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}offer`, obj);
  }

  getAll(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}offer/getAll`, obj);
  }

  getById(id: number, clientId: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}offer/${id}/${clientId}`);
  }

  addToWishlist(offerId: number, userId: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}offer/addToWishlist/${offerId}/${userId}`);
  }

  removeFromWishlist(offerId: number, userId: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}offer/removeFromWishlist/${offerId}/${userId}`);
  }
}
