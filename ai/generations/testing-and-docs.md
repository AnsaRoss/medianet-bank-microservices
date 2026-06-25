# Generacion IA - Pruebas y Documentacion

## Objetivo

Usar IA como apoyo para acelerar la creacion de pruebas automatizadas, checklist de entrega y documentacion tecnica.

## Prompt resumido

Genera escenarios de prueba para un sistema bancario con microservicios Cliente/Persona y Cuenta/Movimientos. Incluye pruebas unitarias de Cliente, pruebas de endpoints y una prueba de integracion para validar que un retiro mayor al saldo retorne "Saldo no disponible".

## Fragmento sugerido por IA

```csharp
[Fact]
public async Task CrearMovimiento_RetiroMayorAlSaldo_DeberiaRetornarSaldoNoDisponible()
{
    var dto = new MovimientoCreateDto
    {
        CuentaId = 1,
        Valor = -2000
    };

    var exception = await Assert.ThrowsAsync<BusinessException>(
        () => service.Crear(dto));

    Assert.Equal("Saldo no disponible", exception.Message);
}
```

## Validacion humana

- Se ajustaron nombres de servicios, repositorios y DTOs a la estructura real del proyecto.
- Se verifico que la prueba apunte a la regla principal de negocio: no permitir saldo negativo.
- Se ejecuto `dotnet test` y el resultado fue 5 pruebas exitosas.

## Resultado esperado

Las pruebas cubren el comportamiento minimo solicitado por la prueba tecnica: entidad Cliente, endpoints y una integracion del flujo de movimiento/saldo.
