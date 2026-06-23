using CuentaMovimiento.Api.Data;
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
        public async Task<ActionResult<Cuenta>> Post(Cuenta cuenta)
        {
            cuenta.SaldoActual = cuenta.SaldoInicial;

            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = cuenta.Id }, cuenta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Cuenta cuenta)
        {
            if (id != cuenta.Id)
                return BadRequest();

            _context.Entry(cuenta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);

            if (cuenta == null)
                return NotFound();

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}