using CuentaMovimiento.Api.DTOs;
using CuentaMovimiento.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CuentaMovimiento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReporteResponseDto>>> Get([FromQuery] ReporteRequestDto request)
        {
            var resultado = await _reporteService.ObtenerEstadoCuenta(request);
            return Ok(resultado);
        }
    }
}
