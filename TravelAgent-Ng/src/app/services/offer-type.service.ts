import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OfferTypeService {

  constructor(private http: HttpClient) { }

  edit(obj: any, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}offerType/${id}`, obj);
  }

  add(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}offerType`, obj);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}offerType/${id}`);
  }

  getAll(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}offerType/getAll`, obj);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}offerType/${id}`);
  }
}
