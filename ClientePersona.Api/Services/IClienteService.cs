using ClientePersona.Api.DTOs;

namespace ClientePersona.Api.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteResponseDto>> ObtenerTodos();
        Task<ClienteResponseDto?> ObtenerPorId(int id);
        Task<ClienteResponseDto> Crear(ClienteCreateDto dto);
        Task<bool> Actualizar(int id, ClienteUpdateDto dto);
        Task<bool> Eliminar(int id);
        Task<ClienteResponseDto> ObtenerPorClienteId(string clienteId);
    }
}
