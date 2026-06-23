using ClientePersona.Api.Data;
using ClientePersona.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientePersona.Api.DTOs;

namespace ClientePersona.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClientePersonaDbContext _context;

        public ClientesController(ClientePersonaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Post(ClienteCreateDto dto)
        {
            var ultimoCliente = await _context.Clientes
                .OrderByDescending(c => c.Id)
                .FirstOrDefaultAsync();

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

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteUpdateDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            cliente.Nombre = dto.Nombre;
            cliente.Genero = dto.Genero;
            cliente.Edad = dto.Edad;
            cliente.Identificacion = dto.Identificacion;
            cliente.Direccion = dto.Direccion;
            cliente.Telefono = dto.Telefono;
            cliente.Contrasena = dto.Contrasena;
            cliente.Estado = dto.Estado;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}