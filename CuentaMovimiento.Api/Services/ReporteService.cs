using CuentaMovimiento.Api.DTOs;
using CuentaMovimiento.Api.Repositories;

namespace CuentaMovimiento.Api.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;
        private readonly IClienteHttpService _clienteHttpService;

        public ReporteService(
            IReporteRepository reporteRepository,
            IClienteHttpService clienteHttpService)
        {
            _reporteRepository = reporteRepository;
            _clienteHttpService = clienteHttpService;
        }

        public async Task<IEnumerable<ReporteResponseDto>> ObtenerEstadoCuenta(ReporteRequestDto request)
        {
            var reporte = (await _reporteRepository.ObtenerEstadoCuenta(request)).ToList();
            var nombreCliente = await _clienteHttpService.ObtenerNombreCliente(request.ClienteId);

            foreach (var item in reporte)
            {
                item.Cliente = nombreCliente ?? item.ClienteId;
            }

            return reporte;
        }
    }
}
