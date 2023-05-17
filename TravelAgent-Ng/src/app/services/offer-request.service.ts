import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OfferRequestService {

  constructor(private http: HttpClient) { }

  edit(obj: any, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}offerRequest/${id}`, obj);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}offerRequest/${id}`);
  }

  add(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}offerRequest`, obj);
  }

  getAll(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}offerRequest/getAll`, obj);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}offerRequest/${id}`);
  }

}
