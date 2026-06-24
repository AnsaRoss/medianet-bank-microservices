namespace CuentaMovimiento.Api.DTOs
{
    public class ReporteRequestDto
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string ClienteId { get; set; }
    }
}
