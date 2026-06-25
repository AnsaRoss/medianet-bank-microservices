import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Reporte } from '../models/reporte';

@Injectable({
  providedIn: 'root'
})
export class ReporteService {

  private apiUrl = `${environment.cuentaApi}/Reportes`;

  constructor(private http: HttpClient) { }

  consultar(fechaInicio: string, fechaFin: string, clienteId: string): Observable<Reporte[]> {
    const params = new HttpParams()
      .set('FechaInicio', fechaInicio)
      .set('FechaFin', fechaFin)
      .set('ClienteId', clienteId);

    return this.http.get<Reporte[]>(this.apiUrl, { params });
  }
}