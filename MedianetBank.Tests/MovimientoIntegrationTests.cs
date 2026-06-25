using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuentaMovimiento.Api.DTOs;
using CuentaMovimiento.Api.Entities;
using CuentaMovimiento.Api.Exceptions;
using CuentaMovimiento.Api.Repositories;
using CuentaMovimiento.Api.Services;
using Moq;

namespace MedianetBank.Tests;

public class MovimientoIntegrationTests
{
    [Fact]
    public async Task Crear_RetiroMayorAlSaldo_DebeLanzarSaldoNoDisponible()
    {
        var cuenta = new Cuenta
        {
            Id = 1,
            NumeroCuenta = "478758",
            TipoCuenta = "Ahorros",
            SaldoInicial = 100,
            SaldoActual = 100,
            Estado = true,
            ClienteId = "CLI0001"
        };

        var movimientoRepositoryMock = new Mock<IMovimientoRepository>();
        var cuentaRepositoryMock = new Mock<ICuentaRepository>();

        cuentaRepositoryMock
            .Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(cuenta);

        var service = new MovimientoService(
            movimientoRepositoryMock.Object,
            cuentaRepositoryMock.Object);

        var dto = new MovimientoCreateDto
        {
            CuentaId = 1,
            Valor = -150
        };

        var exception = await Assert.ThrowsAsync<BusinessException>(() => service.Crear(dto));

        Assert.Equal("Saldo no disponible", exception.Message);

        movimientoRepositoryMock.Verify(r => r.Crear(It.IsAny<Movimiento>()), Times.Never);
        movimientoRepositoryMock.Verify(r => r.GuardarCambios(), Times.Never);
    }

}
