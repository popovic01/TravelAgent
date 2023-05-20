import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(private http: HttpClient) { }

  edit(obj: any, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}reservation/${id}`, obj);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}reservation/${id}`);
  }

  add(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}reservation`, obj);
  }

  getAll(obj: any, id: number): Observable<any> {
    return this.http.post(`${environment.apiUrl}reservation/getAll/${id}`, obj);
  }

}
