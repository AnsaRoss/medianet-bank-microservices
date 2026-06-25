using System.ComponentModel.DataAnnotations;
using ClientePersona.Api.Validations;

namespace ClientePersona.Api.DTOs
{
    public class ClienteCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$", ErrorMessage = "El nombre solo debe contener letras y espacios")]
        [NotPlaceholder(ErrorMessage = "El nombre no puede ser un valor generico de ejemplo")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El genero es obligatorio")]
        [RegularExpression(@"^(Masculino|Femenino|Otro)$", ErrorMessage = "El genero debe ser Masculino, Femenino u Otro")]
        public string Genero { get; set; } = string.Empty;

        [Range(18, 120, ErrorMessage = "La edad debe estar entre 18 y 120")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "La identificacion es obligatoria")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "La identificacion debe contener 10 digitos")]
        public string Identificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La direccion es obligatoria")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "La direccion debe tener entre 5 y 200 caracteres")]
        [NotPlaceholder(ErrorMessage = "La direccion no puede ser un valor generico de ejemplo")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El telefono es obligatorio")]
        [RegularExpression(@"^\d{8,10}$", ErrorMessage = "El teléfono debe contener máximo 10 digitos")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contrasena es obligatoria")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "La contrasena debe tener entre 4 y 50 caracteres")]
        public string Contrasena { get; set; } = string.Empty;

        public bool Estado { get; set; }
    }
}
