using CuentaMovimiento.Api.Data;
using CuentaMovimiento.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CuentaMovimiento.Api.Repositories
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly CuentaMovimientoDbContext _context;

        public ReporteRepository(CuentaMovimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReporteResponseDto>> ObtenerEstadoCuenta(ReporteRequestDto request)
        {
            return await (
                from c in _context.Cuentas
                join m in _context.Movimientos
                    on c.Id equals m.CuentaId
                where c.ClienteId == request.ClienteId
                    && m.Fecha.Date >= request.FechaInicio.Date
                    && m.Fecha.Date <= request.FechaFin.Date
                select new ReporteResponseDto
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
        }
    }
}
