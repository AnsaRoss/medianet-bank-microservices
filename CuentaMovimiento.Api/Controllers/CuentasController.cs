using CuentaMovimiento.Api.DTOs;
using CuentaMovimiento.Api.Entities;
using CuentaMovimiento.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CuentaMovimiento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        public CuentasController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuenta>>> Get()
        {
            var cuentas = await _cuentaService.ObtenerTodos();
            return Ok(cuentas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cuenta>> Get(int id)
        {
            var cuenta = await _cuentaService.ObtenerPorId(id);
            return Ok(cuenta);
        }

        [HttpPost]
        public async Task<ActionResult<Cuenta>> Post(CuentaCreateDto dto)
        {
            var cuenta = await _cuentaService.Crear(dto);
            return CreatedAtAction(nameof(Get), new { id = cuenta.Id }, cuenta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CuentaUpdateDto dto)
        {
            await _cuentaService.Actualizar(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cuentaService.Eliminar(id);
            return NoContent();
        }
    }
}
