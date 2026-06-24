using CuentaMovimiento.Api.DTOs;
using CuentaMovimiento.Api.Entities;
using CuentaMovimiento.Api.Exceptions;
using CuentaMovimiento.Api.Repositories;

namespace CuentaMovimiento.Api.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IClienteHttpService _clienteHttpService;

        public CuentaService(
            ICuentaRepository cuentaRepository,
            IClienteHttpService clienteHttpService)
        {
            _cuentaRepository = cuentaRepository;
            _clienteHttpService = clienteHttpService;
        }

        public async Task<IEnumerable<Cuenta>> ObtenerTodos()
        {
            return await _cuentaRepository.ObtenerTodos();
        }

        public async Task<Cuenta> ObtenerPorId(int id)
        {
            var cuenta = await _cuentaRepository.ObtenerPorId(id);

            if (cuenta == null)
                throw new NotFoundException("Cuenta no encontrada");

            return cuenta;
        }

        public async Task<Cuenta> Crear(CuentaCreateDto dto)
        {
            await ValidarCuenta(dto.NumeroCuenta, dto.SaldoInicial, dto.ClienteId);

            var cuenta = new Cuenta
            {
                NumeroCuenta = dto.NumeroCuenta,
                TipoCuenta = dto.TipoCuenta,
                SaldoInicial = dto.SaldoInicial,
                SaldoActual = dto.SaldoInicial,
                Estado = dto.Estado,
                ClienteId = dto.ClienteId
            };

            await _cuentaRepository.Crear(cuenta);
            await _cuentaRepository.GuardarCambios();

            return cuenta;
        }

        public async Task Actualizar(int id, CuentaUpdateDto dto)
        {
            var cuenta = await ObtenerPorId(id);

            await ValidarNumeroCuenta(dto.NumeroCuenta, id);
            await ValidarCliente(dto.ClienteId);

            cuenta.NumeroCuenta = dto.NumeroCuenta;
            cuenta.TipoCuenta = dto.TipoCuenta;
            cuenta.Estado = dto.Estado;
            cuenta.ClienteId = dto.ClienteId;

            await _cuentaRepository.GuardarCambios();
        }

        public async Task Eliminar(int id)
        {
            var cuenta = await ObtenerPorId(id);

            cuenta.Estado = false;

            await _cuentaRepository.GuardarCambios();
        }

        private async Task ValidarCuenta(string numeroCuenta, decimal saldoInicial, string clienteId)
        {
            if (saldoInicial < 0)
                throw new BusinessException("El saldo inicial no puede ser negativo");

            await ValidarNumeroCuenta(numeroCuenta);
            await ValidarCliente(clienteId);
        }

        private async Task ValidarNumeroCuenta(string numeroCuenta, int? excluirId = null)
        {
            if (await _cuentaRepository.ExisteNumeroCuenta(numeroCuenta, excluirId))
                throw new BusinessException("El numero de cuenta ya existe");
        }

        private async Task ValidarCliente(string clienteId)
        {
            var existeCliente = await _clienteHttpService.ExisteCliente(clienteId);

            if (!existeCliente)
                throw new BusinessException("El cliente no existe");
        }
    }
}
