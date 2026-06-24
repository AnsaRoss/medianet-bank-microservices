using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientePersona.Api.Controllers;
using ClientePersona.Api.DTOs;
using ClientePersona.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MedianetBank.Tests
{
    public class ClientesControllerTests
    {
        [Fact]
        public async Task Get_DebeRetornarOk()
        {
            var serviceMock = new Mock<IClienteService>();

            serviceMock
                .Setup(s => s.ObtenerTodos())
                .ReturnsAsync(new List<ClienteResponseDto>());

            var controller = new ClientesController(serviceMock.Object);

            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
