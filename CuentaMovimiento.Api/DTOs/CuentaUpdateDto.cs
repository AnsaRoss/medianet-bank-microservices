namespace CuentaMovimiento.Api.DTOs
{
    public class CuentaUpdateDto
    {
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public bool Estado { get; set; }
        public string ClienteId { get; set; }
    }
}
