# Generacion IA - Validaciones Backend

## Objetivo

Usar IA como apoyo para identificar validaciones de entrada necesarias en los DTOs y servicios del backend.

## Prompt resumido

Actua como revisor senior de una API bancaria en .NET 6. Revisa DTOs de Cliente, Cuenta, Movimiento y Reporte. Propone validaciones de entrada con DataAnnotations y reglas de negocio para evitar datos vacios, saldos negativos, identificadores invalidos y movimientos sin cuenta.

## Fragmento sugerido por IA

```csharp
public class CuentaCreateDto
{
    [Required]
    [StringLength(20)]
    public string NumeroCuenta { get; set; }

    [Required]
    [StringLength(20)]
    public string TipoCuenta { get; set; }

    [Range(0, double.MaxValue)]
    public decimal SaldoInicial { get; set; }

    public bool Estado { get; set; }

    [Required]
    public string ClienteId { get; set; }
}
```

## Validacion humana

- Se acepto la recomendacion de validar campos obligatorios.
- Se mantuvo la regla de saldo inicial negativo tambien en servicio para proteger el dominio aunque el DTO falle.
- Se identifico como pendiente aplicar estas validaciones de forma consistente en todos los DTOs de entrada.

## Resultado esperado

La API debe rechazar solicitudes incompletas antes de ejecutar reglas de negocio y devolver respuestas coherentes mediante el middleware de excepciones o el modelo de validacion de ASP.NET Core.
