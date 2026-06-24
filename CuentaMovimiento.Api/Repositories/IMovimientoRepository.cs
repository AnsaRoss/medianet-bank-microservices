using CuentaMovimiento.Api.Entities;

namespace CuentaMovimiento.Api.Repositories
{
    public interface IMovimientoRepository
    {
        Task<IEnumerable<Movimiento>> ObtenerTodos();
        Task<Movimiento?> ObtenerPorId(int id);
        Task Crear(Movimiento movimiento);
        Task GuardarCambios();
    }
}
