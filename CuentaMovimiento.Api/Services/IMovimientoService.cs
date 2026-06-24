using CuentaMovimiento.Api.DTOs;
using CuentaMovimiento.Api.Entities;

namespace CuentaMovimiento.Api.Services
{
    public interface IMovimientoService
    {
        Task<IEnumerable<Movimiento>> ObtenerTodos();
        Task<Movimiento> ObtenerPorId(int id);
        Task<Movimiento> Crear(MovimientoCreateDto dto);
        Task<Movimiento> Reversar(int id);
    }
}
