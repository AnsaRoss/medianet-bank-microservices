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
  movimientos: Movimiento[] = [];
  movimientoForm!: FormGroup;
  movimientoEditandoId: number | null = null;
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
    this.cargarMovimientos();
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

  cargarMovimientos(): void {
    this.movimientoService.getAll().subscribe({
      next: data => this.movimientos = data,
      error: () => this.error = 'Error al cargar movimientos'
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
    const request = this.movimientoEditandoId
      ? this.movimientoService.update(this.movimientoEditandoId, movimiento)
      : this.movimientoService.create(movimiento);

    request.subscribe({
      next: () => {
        this.mensaje = this.movimientoEditandoId
          ? 'Movimiento corregido correctamente'
          : 'Movimiento registrado correctamente';
        this.cancelarEdicion();
        this.refrescarDatos();
      },
      error: err => {
        this.error = err.error?.message || err.error || 'Error al registrar movimiento';
      }
    });
  }

  editar(movimiento: Movimiento): void {
    if (!movimiento.id) return;

    this.movimientoEditandoId = movimiento.id;
    this.movimientoForm.patchValue({
      cuentaId: movimiento.cuentaId,
      valor: movimiento.valor
    });
    this.mensaje = 'Corrigiendo movimiento. Se generara reverso del registro original.';
    this.error = '';
  }

  reversar(movimiento: Movimiento): void {
    if (!movimiento.id) return;

    this.movimientoService.delete(movimiento.id).subscribe({
      next: () => {
        this.mensaje = 'Movimiento reversado correctamente';
        this.refrescarDatos();
      },
      error: err => {
        this.error = err.error?.message || err.error || 'Error al reversar movimiento';
      }
    });
  }

  cancelarEdicion(): void {
    this.movimientoEditandoId = null;
    this.movimientoForm.reset({
      cuentaId: '',
      valor: 0
    });
  }

  obtenerTipoMovimiento(): string {
    const valor = this.movimientoForm?.get('valor')?.value;

    if (valor > 0) return 'Deposito';
    if (valor < 0) return 'Retiro';
    return 'Sin movimiento';
  }

  private refrescarDatos(): void {
    this.cargarCuentas();
    this.cargarMovimientos();
  }
}
