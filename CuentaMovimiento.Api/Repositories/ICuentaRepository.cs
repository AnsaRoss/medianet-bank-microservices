using CuentaMovimiento.Api.Entities;

namespace CuentaMovimiento.Api.Repositories
{
    public interface ICuentaRepository
    {
        Task<IEnumerable<Cuenta>> ObtenerTodos();
        Task<Cuenta?> ObtenerPorId(int id);
        Task<bool> ExisteNumeroCuenta(string numeroCuenta, int? excluirId = null);
        Task Crear(Cuenta cuenta);
        Task GuardarCambios();
    }
}
