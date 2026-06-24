using System.Net.Http.Json;

namespace CuentaMovimiento.Api.Services
{
    public class ClienteHttpService : IClienteHttpService
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

        public async Task<string?> ObtenerNombreCliente(string clienteId)
        {
            var response = await _httpClient.GetAsync($"api/Clientes/codigo/{clienteId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var cliente = await response.Content.ReadFromJsonAsync<ClienteResponse>();
            return cliente?.Nombre;
        }

        private class ClienteResponse
        {
            public string? Nombre { get; set; }
        }
    }
}
