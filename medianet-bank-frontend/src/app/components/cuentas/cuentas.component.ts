import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cuenta } from 'src/app/models/cuenta';
import { CuentaService } from 'src/app/services/cuenta.service';
import { Cliente } from 'src/app/models/cliente';
import { ClienteService } from 'src/app/services/cliente.service';

@Component({
  selector: 'app-cuentas',
  templateUrl: './cuentas.component.html',
  styleUrls: ['./cuentas.component.css']
})
export class CuentasComponent implements OnInit {

  cuentas: Cuenta[] = [];
  clientes: Cliente[] = [];
  cuentaForm!: FormGroup;
  editando = false;
  cuentaIdEditando?: number;
  mensaje = '';

  constructor(
    private cuentaService: CuentaService,
    private clienteService: ClienteService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.crearFormulario();
    this.cargarCuentas();
    this.cargarClientes();
  }

  crearFormulario(): void {
    this.cuentaForm = this.fb.group({
      numeroCuenta: ['', [Validators.required, Validators.pattern('^[0-9]{10,20}$')]],
      tipoCuenta: ['', Validators.required],
      saldoInicial: [0, [Validators.required, Validators.min(0)]],
      estado: [true],
      clienteId: ['', Validators.required]
    });
  }

  cargarCuentas(): void {
    this.cuentaService.getAll().subscribe({
      next: data => this.cuentas = data,
      error: () => this.mensaje = 'Error al cargar cuentas'
    });
  }

  cargarClientes(): void {
    this.clienteService.getAll().subscribe({
      next: data => this.clientes = data,
      error: () => this.mensaje = 'Error al cargar clientes'
    });
  }

  guardar(): void {
    if (this.cuentaForm.invalid) {
      this.mensaje = 'Complete los campos requeridos';
      return;
    }

    const cuenta: Cuenta = this.cuentaForm.value;

    if (this.editando && this.cuentaIdEditando) {
      this.cuentaService.update(this.cuentaIdEditando, cuenta).subscribe({
        next: () => {
          this.mensaje = 'Cuenta actualizada correctamente';
          this.cancelar();
          this.cargarCuentas();
        },
        error: () => this.mensaje = 'Error al actualizar cuenta'
      });
    } else {
      this.cuentaService.create(cuenta).subscribe({
        next: () => {
          this.mensaje = 'Cuenta creada correctamente';
          this.cancelar();
          this.cargarCuentas();
        },
        error: () => this.mensaje = 'Error al crear cuenta'
      });
    }
  }

  editar(cuenta: Cuenta): void {
    this.editando = true;
    this.cuentaIdEditando = cuenta.id;

    this.cuentaForm.patchValue({
      numeroCuenta: cuenta.numeroCuenta,
      tipoCuenta: cuenta.tipoCuenta,
      saldoInicial: cuenta.saldoInicial,
      estado: cuenta.estado,
      clienteId: cuenta.clienteId
    });
  }

  eliminar(id?: number): void {
    if (!id) return;

    if (confirm('¿Está segura de eliminar esta cuenta?')) {
      this.cuentaService.delete(id).subscribe({
        next: () => {
          this.mensaje = 'Cuenta eliminada correctamente';
          this.cargarCuentas();
        },
        error: () => this.mensaje = 'Error al eliminar cuenta'
      });
    }
  }

  cancelar(): void {
    this.editando = false;
    this.cuentaIdEditando = undefined;

    this.cuentaForm.reset({
      numeroCuenta: '',
      tipoCuenta: '',
      saldoInicial: 0,
      estado: true,
      clienteId: ''
    });
  }
}