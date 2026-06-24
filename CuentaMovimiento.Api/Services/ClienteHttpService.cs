namespace CuentaMovimiento.Api.Services
{
    public class ClienteHttpService: IClienteHttpService
    {
        private readonly HttpClient _httpClient;

        public ClienteHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ExisteCliente(string clienteId)
        {
            var response = await _httpClient.GetAsync($"api/Clientes/codigo/{clienteId}");
            return response.IsSuccessStatusCode;
        }
    }
}
