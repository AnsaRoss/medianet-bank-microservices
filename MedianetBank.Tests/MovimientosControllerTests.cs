using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuentaMovimiento.Api.Controllers;
using CuentaMovimiento.Api.Entities;
using CuentaMovimiento.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MedianetBank.Tests
{
    public class MovimientosControllerTests
    {
        [Fact]
        public async Task Get_DebeRetornarOk()
        {
            var serviceMock = new Mock<IMovimientoService>();

            serviceMock
                .Setup(s => s.ObtenerTodos())
                .ReturnsAsync(new List<Movimiento>());

            var controller = new MovimientosController(serviceMock.Object);

            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}