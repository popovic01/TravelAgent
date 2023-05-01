import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OfferTypeService {

  constructor(private http: HttpClient) { }

  getAll(obj: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}offerType/getAll`, obj);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}offerType/${id}`);
  }
}
