using CuentaMovimiento.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuentaMovimiento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly CuentaMovimientoDbContext _context;

        public ReportesController(CuentaMovimientoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            DateTime fechaInicio,
            DateTime fechaFin,
            int clienteId)
        {
            var resultado = await (
                from c in _context.Cuentas
                join m in _context.Movimientos
                    on c.Id equals m.CuentaId
                where c.ClienteId == clienteId
                    && m.Fecha >= fechaInicio
                    && m.Fecha <= fechaFin
                select new
                {
                    Fecha = m.Fecha,
                    ClienteId = c.ClienteId,
                    NumeroCuenta = c.NumeroCuenta,
                    TipoCuenta = c.TipoCuenta,
                    SaldoInicial = c.SaldoInicial,
                    Estado = c.Estado,
                    Movimiento = m.Valor,
                    SaldoDisponible = m.Saldo
                }
            ).ToListAsync();

            return Ok(resultado);
        }
    }
}