using CuentaMovimiento.Api.Data;
using CuentaMovimiento.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuentaMovimiento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly CuentaMovimientoDbContext _context;

        public MovimientosController(CuentaMovimientoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimiento>>> Get()
        {
            return await _context.Movimientos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> Get(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);

            if (movimiento == null)
                return NotFound();

            return movimiento;
        }

        [HttpPost]
        public async Task<ActionResult<Movimiento>> Post(Movimiento movimiento)
        {
            var cuenta = await _context.Cuentas.FindAsync(movimiento.CuentaId);

            if (cuenta == null)
                return BadRequest("La cuenta no existe.");

            var nuevoSaldo = cuenta.SaldoActual + movimiento.Valor;

            if (nuevoSaldo < 0)
                return BadRequest("Saldo no disponible");

            movimiento.Fecha = movimiento.Fecha == default ? DateTime.Now : movimiento.Fecha;
            movimiento.TipoMovimiento = movimiento.Valor >= 0 ? "Deposito" : "Retiro";
            movimiento.Saldo = nuevoSaldo;

            cuenta.SaldoActual = nuevoSaldo;

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = movimiento.Id }, movimiento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Movimiento movimiento)
        {
            if (id != movimiento.Id)
                return BadRequest();

            _context.Entry(movimiento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);

            if (movimiento == null)
                return NotFound();

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}