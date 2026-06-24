using ClientePersona.Api.Data;
using ClientePersona.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientePersona.Api.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientePersonaDbContext _context;

        public ClienteRepository(ClientePersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodos()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> ObtenerPorId(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente?> ObtenerUltimo()
        {
            return await _context.Clientes
                .OrderByDescending(c => c.Id)
                .FirstOrDefaultAsync();
        }

        public async Task Crear(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Cliente?> ObtenerPorClienteId(string clienteId)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
        }
    }
}