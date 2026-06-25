import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cuenta } from 'src/app/models/cuenta';
import { Movimiento } from 'src/app/models/movimiento';
import { CuentaService } from 'src/app/services/cuenta.service';
import { MovimientoService } from 'src/app/services/movimiento.service';

@Component({
  selector: 'app-movimientos',
  templateUrl: './movimientos.component.html',
  styleUrls: ['./movimientos.component.css']
})
export class MovimientosComponent implements OnInit {

  cuentas: Cuenta[] = [];
  movimientoForm!: FormGroup;
  mensaje = '';
  error = '';

  constructor(
    private fb: FormBuilder,
    private cuentaService: CuentaService,
    private movimientoService: MovimientoService
  ) { }

  ngOnInit(): void {
    this.crearFormulario();
    this.cargarCuentas();
  }

  crearFormulario(): void {
    this.movimientoForm = this.fb.group({
      cuentaId: ['', Validators.required],
      valor: [null, [Validators.required, Validators.pattern('^-?[0-9]+(\\.[0-9]{1,2})?$')]]
    });
  }

  cargarCuentas(): void {
    this.cuentaService.getAll().subscribe({
      next: data => this.cuentas = data.filter(c => c.estado),
      error: () => this.error = 'Error al cargar cuentas'
    });
  }

  guardar(): void {
    this.mensaje = '';
    this.error = '';

    if (this.movimientoForm.invalid) {
      this.error = 'Complete los campos requeridos';
      return;
    }

    const movimiento: Movimiento = this.movimientoForm.value;

    this.movimientoService.create(movimiento).subscribe({
      next: () => {
        this.mensaje = 'Movimiento registrado correctamente';
        this.movimientoForm.reset({
          cuentaId: '',
          valor: 0
        });
        this.cargarCuentas();
      },
      error: err => {
        this.error = err.error?.message || err.error || 'Error al registrar movimiento';
      }
    });
  }

  obtenerTipoMovimiento(): string {
    const valor = this.movimientoForm?.get('valor')?.value;

    if (valor > 0) return 'Depósito';
    if (valor < 0) return 'Retiro';
    return 'Sin movimiento';
  }
}