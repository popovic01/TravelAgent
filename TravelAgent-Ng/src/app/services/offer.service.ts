import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OfferService {

  constructor(private http: HttpClient) { }

  add(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}offer`, obj);
  }

  getAll(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}offer/getAll`, obj);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}offer/${id}`);
  }
}
