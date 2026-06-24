using Microsoft.AspNetCore.Mvc;
using ClientePersona.Api.DTOs;
using ClientePersona.Api.Services;

namespace ClientePersona.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> Get()
        {
            var clientes = await _clienteService.ObtenerTodos();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> Get(int id)
        {
            var cliente = await _clienteService.ObtenerPorId(id);
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteResponseDto>> Post(ClienteCreateDto dto)
        {
            var cliente = await _clienteService.Crear(dto);
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteUpdateDto dto)
        {
            await _clienteService.Actualizar(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteService.Eliminar(id);
            return NoContent();
        }
        [HttpGet("codigo/{clienteId}")]
        public async Task<ActionResult<ClienteResponseDto>> GetByClienteId(string clienteId)
        {
            var cliente = await _clienteService.ObtenerPorClienteId(clienteId);
            return Ok(cliente);
        }
    }
}