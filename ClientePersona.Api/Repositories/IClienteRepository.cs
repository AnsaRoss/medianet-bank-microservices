using ClientePersona.Api.Entities;

namespace ClientePersona.Api.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObtenerTodos();
        Task<Cliente?> ObtenerPorId(int id);
        Task<Cliente?> ObtenerUltimo();
        Task Crear(Cliente cliente);
        Task GuardarCambios();
        Task<Cliente?> ObtenerPorClienteId(string clienteId);
    }
}
