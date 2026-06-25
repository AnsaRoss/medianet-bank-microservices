export interface Reporte {
  fecha: string;
  clienteId: string;
  cliente: string;
  numeroCuenta: string;
  tipoCuenta: string;
  saldoInicial: number;
  estado: boolean;
  movimiento: number;
  saldoDisponible: number;
}