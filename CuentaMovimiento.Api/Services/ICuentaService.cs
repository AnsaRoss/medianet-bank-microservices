using CuentaMovimiento.Api.DTOs;
using CuentaMovimiento.Api.Entities;

namespace CuentaMovimiento.Api.Services
{
    public interface ICuentaService
    {
        Task<IEnumerable<Cuenta>> ObtenerTodos();
        Task<Cuenta> ObtenerPorId(int id);
        Task<Cuenta> Crear(CuentaCreateDto dto);
        Task Actualizar(int id, CuentaUpdateDto dto);
        Task Eliminar(int id);
    }
}
