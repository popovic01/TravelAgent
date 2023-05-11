import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private http: HttpClient) { }

  edit(obj: any, id: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}location/${id}`, obj);
  }

  add(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}location`, obj);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}location/${id}`);
  }

  getAll(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}location/getAll`, obj);
  }
  
}
