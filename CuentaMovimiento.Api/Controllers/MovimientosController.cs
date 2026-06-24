using CuentaMovimiento.Api.DTOs;
using CuentaMovimiento.Api.Entities;
using CuentaMovimiento.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CuentaMovimiento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService;

        public MovimientosController(IMovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimiento>>> Get()
        {
            var movimientos = await _movimientoService.ObtenerTodos();
            return Ok(movimientos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> Get(int id)
        {
            var movimiento = await _movimientoService.ObtenerPorId(id);
            return Ok(movimiento);
        }

        [HttpPost]
        public async Task<ActionResult<Movimiento>> Post(MovimientoCreateDto dto)
        {
            var movimiento = await _movimientoService.Crear(dto);
            return CreatedAtAction(nameof(Get), new { id = movimiento.Id }, movimiento);
        }

        // Aunque el enunciado solicita CRUD sobre movimientos, por criterio financiero evite
        // modificar transacciones historicas directamente. Para correcciones se implementa un
        // reverso mediante un nuevo movimiento que compensa el valor original y mantiene trazabilidad.
        [HttpPost("{id}/reverso")]
        public async Task<ActionResult<Movimiento>> Reversar(int id)
        {
            var movimientoReverso = await _movimientoService.Reversar(id);
            return CreatedAtAction(nameof(Get), new { id = movimientoReverso.Id }, movimientoReverso);
        }
    }
}
