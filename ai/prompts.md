# Prompts Utilizados

## Arquitectura

Se utilizó Inteligencia Artificial para proponer una arquitectura basada en microservicios utilizando:

- ClientePersona.Api
- CuentaMovimiento.Api

con separación por Controllers, Services, Repositories y Entity Framework Core.

## Comunicación entre Microservicios

Se utilizaron prompts para definir la comunicación entre microservicios mediante HttpClient para validar la existencia de clientes antes de crear o actualizar cuentas.

## Validaciones de Negocio

La IA fue utilizada para revisar:

- Validación de saldo disponible.
- Validación de cliente existente.
- Validación de número de cuenta único.
- Manejo centralizado de excepciones.

## Pruebas Automatizadas

Se utilizaron prompts para generar ejemplos y estructuras de:

- Pruebas unitarias con xUnit.
- Uso de Moq para repositorios.
- Pruebas de endpoints.
- Pruebas de integración.

## Documentación

La IA fue utilizada para apoyar la elaboración de:

- README.
- Registro de decisiones técnicas.
- Estructura de entrega del proyecto.