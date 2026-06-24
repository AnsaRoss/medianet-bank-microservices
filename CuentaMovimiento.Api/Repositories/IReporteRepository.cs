using CuentaMovimiento.Api.DTOs;

namespace CuentaMovimiento.Api.Repositories
{
    public interface IReporteRepository
    {
        Task<IEnumerable<ReporteResponseDto>> ObtenerEstadoCuenta(ReporteRequestDto request);
    }
}
