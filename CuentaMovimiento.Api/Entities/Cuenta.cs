namespace CuentaMovimiento.Api.Entities
{
    public class Cuenta
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoActual { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }
    }
}
