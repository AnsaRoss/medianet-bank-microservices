using ClientePersona.Api.Data;
using ClientePersona.Api.DTOs;
using ClientePersona.Api.Entities;
using ClientePersona.Api.Repositories;
using ClientePersona.Api.Exceptions;

namespace ClientePersona.Api.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteResponseDto>> ObtenerTodos()
        {
            var clientes = await _clienteRepository.ObtenerTodos();

            return clientes.Select(MapToResponse);
        }

        public async Task<ClienteResponseDto> ObtenerPorId(int id)
        {
            var cliente = await _clienteRepository.ObtenerPorId(id);
            if (cliente == null)
                throw new NotFoundException("Cliente no encontrado");

            return MapToResponse(cliente);

        }

        public async Task<ClienteResponseDto> Crear(ClienteCreateDto dto)
        {
            var ultimoCliente = await _clienteRepository.ObtenerUltimo();

            var siguienteNumero = ultimoCliente == null
                ? 1
                : ultimoCliente.Id + 1;

            var cliente = new Cliente
            {
                ClienteId = $"CLI{siguienteNumero:D4}",
                Nombre = dto.Nombre,
                Genero = dto.Genero,
                Edad = dto.Edad,
                Identificacion = dto.Identificacion,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Contrasena = dto.Contrasena,
                Estado = dto.Estado
            };

            await _clienteRepository.Crear(cliente);
            await _clienteRepository.GuardarCambios();

            return MapToResponse(cliente);
        }

        public async Task<bool> Actualizar(int id, ClienteUpdateDto dto)
        {
            var cliente = await _clienteRepository.ObtenerPorId(id);

            if (cliente == null)
                throw new NotFoundException("Cliente no encontrado");

            cliente.Nombre = dto.Nombre;
            cliente.Genero = dto.Genero;
            cliente.Edad = dto.Edad;
            cliente.Identificacion = dto.Identificacion;
            cliente.Direccion = dto.Direccion;
            cliente.Telefono = dto.Telefono;
            cliente.Contrasena = dto.Contrasena;
            cliente.Estado = dto.Estado;

            await _clienteRepository.GuardarCambios();

            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            var cliente = await _clienteRepository.ObtenerPorId(id);

            if (cliente == null)
                throw new NotFoundException("Cliente no encontrado");

            cliente.Estado = false;

            await _clienteRepository.GuardarCambios();

            return true;
        }

        private static ClienteResponseDto MapToResponse(Cliente cliente)
        {
            return new ClienteResponseDto
            {
                Id = cliente.Id,
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Genero = cliente.Genero,
                Edad = cliente.Edad,
                Identificacion = cliente.Identificacion,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono,
                Estado = cliente.Estado
            };
        }
        public async Task<ClienteResponseDto> ObtenerPorClienteId(string clienteId)
        {
            var cliente = await _clienteRepository.ObtenerPorClienteId(clienteId);

            if (cliente == null)
                throw new NotFoundException("Cliente no encontrado");

            return MapToResponse(cliente);
        }
    }
}