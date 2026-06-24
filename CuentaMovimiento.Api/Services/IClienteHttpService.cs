namespace CuentaMovimiento.Api.Services
{
    public interface IClienteHttpService
    {
        Task<bool> ExisteCliente(string clienteId);
    }
}
