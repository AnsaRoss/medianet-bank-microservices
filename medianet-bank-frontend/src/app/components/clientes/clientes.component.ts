import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cliente } from 'src/app/models/cliente';
import { ClienteService } from 'src/app/services/cliente.service';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {

  clientes: Cliente[] = [];
  clienteForm!: FormGroup;
  editando = false;
  clienteIdEditando?: number;
  mensaje = '';

  constructor(
    private clienteService: ClienteService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.crearFormulario();
    this.cargarClientes();
  }

  crearFormulario(): void {
    this.clienteForm = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(3), Validators.pattern('^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$')]],
      genero: ['', Validators.required],
      edad: [18, [Validators.required, Validators.min(18), Validators.max(120)]],
      identificacion: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      direccion: ['', [Validators.required, Validators.minLength(5)]],
      telefono: ['', [Validators.required, Validators.pattern('^09[0-9]{8}$')]],
      contrasena: ['', [Validators.required, Validators.minLength(4)]],
      estado: [true]
    });
  }

  cargarClientes(): void {
    this.clienteService.getAll().subscribe({
      next: data => this.clientes = data,
      error: err => this.mensaje = 'Error al cargar clientes'
    });
  }

  guardar(): void {
    if (this.clienteForm.invalid) {
      this.mensaje = 'Complete los campos requeridos';
      return;
    }

    const cliente: Cliente = this.clienteForm.value;

    if (this.editando && this.clienteIdEditando) {
      this.clienteService.update(this.clienteIdEditando, cliente).subscribe({
        next: () => {
          this.mensaje = 'Cliente actualizado correctamente';
          this.cancelar();
          this.cargarClientes();
        },
        error: err => this.mensaje = 'Error al actualizar cliente'
      });
    } else {
      this.clienteService.create(cliente).subscribe({
        next: () => {
          this.mensaje = 'Cliente creado correctamente';
          this.cancelar();
          this.cargarClientes();
        },
        error: err => this.mensaje = 'Error al crear cliente'
      });
    }
  }

  editar(cliente: Cliente): void {
    this.editando = true;
    this.clienteIdEditando = cliente.id;

    this.clienteForm.patchValue({
      nombre: cliente.nombre,
      genero: cliente.genero,
      edad: cliente.edad,
      identificacion: cliente.identificacion,
      direccion: cliente.direccion,
      telefono: cliente.telefono,
      contrasena: '',
      estado: cliente.estado
    });
  }

  eliminar(id?: number): void {
    if (!id) return;

    if (confirm('¿Está segura de eliminar este cliente?')) {
      this.clienteService.delete(id).subscribe({
        next: () => {
          this.mensaje = 'Cliente eliminado correctamente';
          this.cargarClientes();
        },
        error: err => this.mensaje = 'Error al eliminar cliente'
      });
    }
  }

  cancelar(): void {
    this.editando = false;
    this.clienteIdEditando = undefined;
    this.clienteForm.reset({
      nombre: '',
      genero: '',
      edad: 0,
      identificacion: '',
      direccion: '',
      telefono: '',
      contrasena: '',
      estado: true
    });
  }
}