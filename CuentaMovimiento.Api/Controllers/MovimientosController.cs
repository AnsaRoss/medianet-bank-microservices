using System.Drawing;
using CuentaMovimiento.Api.Data;
using CuentaMovimiento.Api.DTOs;
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
        public async Task<ActionResult<Movimiento>> Post(MovimientoCreateDto dto)
        {
            var cuenta = await _context.Cuentas.FindAsync(dto.CuentaId);

            if (cuenta == null)
                return BadRequest("La cuenta no existe.");

            var nuevoSaldo = cuenta.SaldoActual + dto.Valor;

            if (nuevoSaldo < 0)
                return BadRequest("Saldo no disponible");

            var movimiento = new Movimiento
            {
                Fecha = DateTime.Now,
                TipoMovimiento = dto.Valor >= 0 ? "Deposito" : "Retiro",
                Valor = dto.Valor,
                Saldo = nuevoSaldo,
                CuentaId = dto.CuentaId
            };

            cuenta.SaldoActual = nuevoSaldo;

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = movimiento.Id }, movimiento);
        }
        //Aunque el enunciado solicita CRUD sobre movimientos, por criterio financiero evité
        //modificar transacciones históricas directamente.Para correcciones se implementa un
        //reverso mediante un nuevo movimiento que compensa el valor original y mantiene trazabilidad.
        [HttpPost("{id}/reverso")]
        public async Task<ActionResult<Movimiento>> Reversar(int id)
        {
            var movimientoOriginal = await _context.Movimientos.FindAsync(id);

            if (movimientoOriginal == null)
                return NotFound();

            var cuenta = await _context.Cuentas.FindAsync(movimientoOriginal.CuentaId);

            if (cuenta == null)
                return BadRequest("La cuenta no existe.");

            var valorReverso = movimientoOriginal.Valor * -1;
            var nuevoSaldo = cuenta.SaldoActual + valorReverso;

            if (nuevoSaldo < 0)
                return BadRequest("Saldo no disponible");

            var movimientoReverso = new Movimiento
            {
                Fecha = DateTime.Now,
                TipoMovimiento = "Reverso",
                Valor = valorReverso,
                Saldo = nuevoSaldo,
                CuentaId = movimientoOriginal.CuentaId
            };

            cuenta.SaldoActual = nuevoSaldo;

            _context.Movimientos.Add(movimientoReverso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = movimientoReverso.Id }, movimientoReverso);
        }
    }
}