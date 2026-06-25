using System.ComponentModel.DataAnnotations;

namespace CuentaMovimiento.Api.DTOs
{
    public class ReporteRequestDto
    {
        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        [RegularExpression(@"^CLI\d{4,}$", ErrorMessage = "El cliente debe tener formato CLI0001")]
        public string ClienteId { get; set; } = string.Empty;
    }
}
