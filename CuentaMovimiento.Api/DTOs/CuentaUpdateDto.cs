using System.ComponentModel.DataAnnotations;

namespace CuentaMovimiento.Api.DTOs
{
    public class CuentaUpdateDto
    {
        [Required(ErrorMessage = "El numero de cuenta es obligatorio")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "El numero de cuenta debe contener solo numeros y maximo 10 digitos")]
        public string NumeroCuenta { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de cuenta es obligatorio")]
        [RegularExpression(@"^(Ahorro|Ahorros|Corriente)$", ErrorMessage = "El tipo de cuenta debe ser Ahorro, Ahorros o Corriente")]
        public string TipoCuenta { get; set; } = string.Empty;

        public bool Estado { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        [RegularExpression(@"^CLI\d{4,}$", ErrorMessage = "El cliente debe tener formato CLI0001")]
        public string ClienteId { get; set; } = string.Empty;
    }
}
