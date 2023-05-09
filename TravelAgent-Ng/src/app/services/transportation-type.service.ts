import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TransportationTypeService {

  constructor(private http: HttpClient) { }

  edit(obj: any, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}transportationType/${id}`, obj);
  }

  add(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}transportationType`, obj);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}transportationType/${id}`);
  }

  getAll(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}transportationType/getAll`, obj);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}transportationType/${id}`);
  }
}
