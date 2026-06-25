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

        // Correccion auditable: se reversa el movimiento original y se registra uno nuevo.
        [HttpPut("{id}")]
        public async Task<ActionResult<Movimiento>> Put(int id, MovimientoUpdateDto dto)
        {
            var movimientoCorregido = await _movimientoService.Corregir(id, dto);
            return Ok(movimientoCorregido);
        }

        // Borrado auditable: no elimina fisicamente; genera un movimiento de reverso.
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movimiento>> Delete(int id)
        {
            var movimientoReverso = await _movimientoService.Reversar(id);
            return Ok(movimientoReverso);
        }

        [HttpPost("{id}/reverso")]
        public async Task<ActionResult<Movimiento>> Reversar(int id)
        {
            var movimientoReverso = await _movimientoService.Reversar(id);
            return CreatedAtAction(nameof(Get), new { id = movimientoReverso.Id }, movimientoReverso);
        }
    }
}
