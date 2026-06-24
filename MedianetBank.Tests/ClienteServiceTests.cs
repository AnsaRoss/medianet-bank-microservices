using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientePersona.Api.DTOs;
using ClientePersona.Api.Entities;
using ClientePersona.Api.Exceptions;
using ClientePersona.Api.Repositories;
using ClientePersona.Api.Services;
using Moq;

public class ClienteServiceTests
{
    [Fact]
    public async Task Crear_DebeGenerarClienteIdAutomatico()
    {
        var repositoryMock = new Mock<IClienteRepository>();

        repositoryMock
            .Setup(r => r.ObtenerUltimo())
            .ReturnsAsync((Cliente?)null);

        var service = new ClienteService(repositoryMock.Object);

        var dto = new ClienteCreateDto
        {
            Nombre = "Jose Lema",
            Genero = "M",
            Edad = 30,
            Identificacion = "1234567890",
            Direccion = "Otavalo sn y principal",
            Telefono = "098254785",
            Contrasena = "1234",
            Estado = true
        };

        var resultado = await service.Crear(dto);

        Assert.Equal("CLI0001", resultado.ClienteId);
        Assert.Equal("Jose Lema", resultado.Nombre);

        repositoryMock.Verify(r => r.Crear(It.IsAny<Cliente>()), Times.Once);
        repositoryMock.Verify(r => r.GuardarCambios(), Times.Once);
    }

    [Fact]
    public async Task ObtenerPorId_CuandoNoExiste_DebeLanzarNotFoundException()
    {
        var repositoryMock = new Mock<IClienteRepository>();

        repositoryMock
            .Setup(r => r.ObtenerPorId(99))
            .ReturnsAsync((Cliente?)null);

        var service = new ClienteService(repositoryMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => service.ObtenerPorId(99));
    }
}