using System.ComponentModel.DataAnnotations;

namespace CuentaMovimiento.Api.DTOs
{
    public class MovimientoUpdateDto
    {
        [Range(typeof(decimal), "-999999999999", "999999999999", ErrorMessage = "El valor del movimiento esta fuera del rango permitido")]
        public decimal Valor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cuenta es obligatoria")]
        public int CuentaId { get; set; }
    }
}
