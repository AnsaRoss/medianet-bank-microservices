using CuentaMovimiento.Api.Data;
using CuentaMovimiento.Api.DTOs;
using CuentaMovimiento.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuentaMovimiento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly CuentaMovimientoDbContext _context;

        public CuentasController(CuentaMovimientoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuenta>>> Get()
        {
            return await _context.Cuentas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cuenta>> Get(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);

            if (cuenta == null)
                return NotFound();

            return cuenta;
        }

        [HttpPost]
        public async Task<ActionResult<Cuenta>> Post(CuentaCreateDto dto)
        {
            var cuenta = new Cuenta
            {
                NumeroCuenta = dto.NumeroCuenta,
                TipoCuenta = dto.TipoCuenta,
                SaldoInicial = dto.SaldoInicial,
                SaldoActual = dto.SaldoInicial,
                Estado = dto.Estado,
                ClienteId = dto.ClienteId
            };

            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = cuenta.Id }, cuenta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CuentaUpdateDto dto)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);

            if (cuenta == null)
                return NotFound();

            cuenta.NumeroCuenta = dto.NumeroCuenta;
            cuenta.TipoCuenta = dto.TipoCuenta;
            cuenta.Estado = dto.Estado;
            cuenta.ClienteId = dto.ClienteId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);

            if (cuenta == null)
                return NotFound();

            cuenta.Estado = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}