using System.ComponentModel.DataAnnotations;

namespace CuentaMovimiento.Api.DTOs
{
    public class CuentaCreateDto
    {
        [Required(ErrorMessage = "El numero de cuenta es obligatorio")]
        [RegularExpression(@"^\d{5,15}$", ErrorMessage = "El numero de cuenta debe contener solo numeros y entre 5 y 15 digitos")]
        public string NumeroCuenta { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de cuenta es obligatorio")]
        [RegularExpression(@"^(Ahorro|Ahorros|Corriente)$", ErrorMessage = "El tipo de cuenta debe ser Ahorro, Ahorros o Corriente")]
        public string TipoCuenta { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "El saldo inicial no puede ser negativo")]
        public decimal SaldoInicial { get; set; }

        public bool Estado { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        [RegularExpression(@"^CLI\d{4,}$", ErrorMessage = "El cliente debe tener formato CLI0001")]
        public string ClienteId { get; set; } = string.Empty;
    }
}
