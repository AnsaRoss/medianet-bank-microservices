using CuentaMovimiento.Api.DTOs;
using CuentaMovimiento.Api.Entities;
using CuentaMovimiento.Api.Exceptions;
using CuentaMovimiento.Api.Repositories;

namespace CuentaMovimiento.Api.Services
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly ICuentaRepository _cuentaRepository;

        public MovimientoService(
            IMovimientoRepository movimientoRepository,
            ICuentaRepository cuentaRepository)
        {
            _movimientoRepository = movimientoRepository;
            _cuentaRepository = cuentaRepository;
        }

        public async Task<IEnumerable<Movimiento>> ObtenerTodos()
        {
            return await _movimientoRepository.ObtenerTodos();
        }

        public async Task<Movimiento> ObtenerPorId(int id)
        {
            var movimiento = await _movimientoRepository.ObtenerPorId(id);

            if (movimiento == null)
                throw new NotFoundException("Movimiento no encontrado");

            return movimiento;
        }

        public async Task<Movimiento> Crear(MovimientoCreateDto dto)
        {
            var cuenta = await ObtenerCuenta(dto.CuentaId);
            var nuevoSaldo = CalcularNuevoSaldo(cuenta, dto.Valor);

            var movimiento = new Movimiento
            {
                Fecha = DateTime.Now,
                TipoMovimiento = dto.Valor >= 0 ? "Deposito" : "Retiro",
                Valor = dto.Valor,
                Saldo = nuevoSaldo,
                CuentaId = dto.CuentaId
            };

            cuenta.SaldoActual = nuevoSaldo;

            await _movimientoRepository.Crear(movimiento);
            await _movimientoRepository.GuardarCambios();

            return movimiento;
        }

        public async Task<Movimiento> Reversar(int id)
        {
            var movimientoOriginal = await ObtenerPorId(id);
            var cuenta = await ObtenerCuenta(movimientoOriginal.CuentaId);
            var valorReverso = movimientoOriginal.Valor * -1;
            var nuevoSaldo = CalcularNuevoSaldo(cuenta, valorReverso);

            var movimientoReverso = new Movimiento
            {
                Fecha = DateTime.Now,
                TipoMovimiento = "Reverso",
                Valor = valorReverso,
                Saldo = nuevoSaldo,
                CuentaId = movimientoOriginal.CuentaId
            };

            cuenta.SaldoActual = nuevoSaldo;

            await _movimientoRepository.Crear(movimientoReverso);
            await _movimientoRepository.GuardarCambios();

            return movimientoReverso;
        }

        private async Task<Cuenta> ObtenerCuenta(int cuentaId)
        {
            var cuenta = await _cuentaRepository.ObtenerPorId(cuentaId);

            if (cuenta == null)
                throw new BusinessException("La cuenta no existe");

            return cuenta;
        }

        private static decimal CalcularNuevoSaldo(Cuenta cuenta, decimal valor)
        {
            var nuevoSaldo = cuenta.SaldoActual + valor;

            if (nuevoSaldo < 0)
                throw new BusinessException("Saldo no disponible");

            return nuevoSaldo;
        }
    }
}
