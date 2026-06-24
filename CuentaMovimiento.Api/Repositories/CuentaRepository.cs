using CuentaMovimiento.Api.Data;
using CuentaMovimiento.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuentaMovimiento.Api.Repositories
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly CuentaMovimientoDbContext _context;

        public CuentaRepository(CuentaMovimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cuenta>> ObtenerTodos()
        {
            return await _context.Cuentas.ToListAsync();
        }

        public async Task<Cuenta?> ObtenerPorId(int id)
        {
            return await _context.Cuentas.FindAsync(id);
        }

        public async Task<bool> ExisteNumeroCuenta(string numeroCuenta, int? excluirId = null)
        {
            return await _context.Cuentas.AnyAsync(c =>
                c.NumeroCuenta == numeroCuenta &&
                (!excluirId.HasValue || c.Id != excluirId.Value));
        }

        public async Task Crear(Cuenta cuenta)
        {
            await _context.Cuentas.AddAsync(cuenta);
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }
    }
}
