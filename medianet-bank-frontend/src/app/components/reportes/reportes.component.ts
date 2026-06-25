import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cliente } from 'src/app/models/cliente';
import { Reporte } from 'src/app/models/reporte';
import { ClienteService } from 'src/app/services/cliente.service';
import { ReporteService } from 'src/app/services/reporte.service';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.component.html',
  styleUrls: ['./reportes.component.css']
})
export class ReportesComponent implements OnInit {

  reporteForm!: FormGroup;
  clientes: Cliente[] = [];
  reportes: Reporte[] = [];
  mensaje = '';
  error = '';

  constructor(
    private fb: FormBuilder,
    private clienteService: ClienteService,
    private reporteService: ReporteService
  ) { }

  ngOnInit(): void {
    this.crearFormulario();
    this.cargarClientes();
  }

  crearFormulario(): void {
    this.reporteForm = this.fb.group({
      fechaInicio: ['', Validators.required],
      fechaFin: ['', Validators.required],
      clienteId: ['', Validators.required]
    });
  }

  cargarClientes(): void {
    this.clienteService.getAll().subscribe({
      next: data => this.clientes = data,
      error: () => this.error = 'Error al cargar clientes'
    });
  }

  consultar(): void {
    this.mensaje = '';
    this.error = '';
    this.reportes = [];

    if (this.reporteForm.invalid) {
      this.error = 'Complete los filtros requeridos';
      return;
    }

    const { fechaInicio, fechaFin, clienteId } = this.reporteForm.value;

    if (fechaInicio > fechaFin) {
      this.error = 'La fecha inicio no puede ser mayor que la fecha fin';
      return;
    }

    this.reporteService.consultar(fechaInicio, fechaFin, clienteId).subscribe({
      next: data => {
        this.reportes = data;

        if (data.length === 0) {
          this.mensaje = 'No se encontraron movimientos para los filtros seleccionados';
        }
      },
      error: () => this.error = 'Error al consultar reporte'
    });
  }
}