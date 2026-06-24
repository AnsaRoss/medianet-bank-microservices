using CuentaMovimiento.Api.Data;
using CuentaMovimiento.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuentaMovimiento.Api.Repositories
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly CuentaMovimientoDbContext _context;

        public MovimientoRepository(CuentaMovimientoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movimiento>> ObtenerTodos()
        {
            return await _context.Movimientos.ToListAsync();
        }

        public async Task<Movimiento?> ObtenerPorId(int id)
        {
            return await _context.Movimientos.FindAsync(id);
        }

        public async Task Crear(Movimiento movimiento)
        {
            await _context.Movimientos.AddAsync(movimiento);
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }
    }
}
