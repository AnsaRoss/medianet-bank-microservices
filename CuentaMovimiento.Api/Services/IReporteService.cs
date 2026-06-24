using CuentaMovimiento.Api.DTOs;

namespace CuentaMovimiento.Api.Services
{
    public interface IReporteService
    {
        Task<IEnumerable<ReporteResponseDto>> ObtenerEstadoCuenta(ReporteRequestDto request);
    }
}
