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

  create(movimiento: Movimiento): Observable<any> {
    return this.http.post<any>(this.apiUrl, movimiento);
  }
}