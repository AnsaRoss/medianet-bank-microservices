import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Movimiento } from '../models/movimiento';

@Injectable({
  providedIn: 'root'
})
export class MovimientoService {

  private apiUrl = `${environment.cuentaApi}/Movimientos`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Movimiento[]> {
    return this.http.get<Movimiento[]>(this.apiUrl);
  }

  create(movimiento: Movimiento): Observable<any> {
    return this.http.post<any>(this.apiUrl, movimiento);
  }

  update(id: number, movimiento: Movimiento): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, movimiento);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
