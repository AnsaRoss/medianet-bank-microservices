# Decisiones Técnicas

## Arquitectura

Se decidió implementar una arquitectura basada en microservicios separando los dominios:

- ClientePersona.Api
- CuentaMovimiento.Api

Cada servicio posee su propia base de datos y responsabilidades independientes.

## Comunicación entre Microservicios

Se implementó comunicación mediante HttpClient desde CuentaMovimiento.Api hacia ClientePersona.Api para validar la existencia de clientes antes de crear o actualizar cuentas.

## Identificador de Negocio

Se decidió utilizar ClienteId de negocio (ejemplo: CLI0001) para la comunicación entre microservicios en lugar del identificador interno autoincremental de base de datos.

## Movimientos Bancarios

Se decidió no permitir la modificación directa de movimientos históricos.

Para mantener la trazabilidad se implementó un endpoint de reverso que genera un nuevo movimiento compensatorio.

## Manejo de Excepciones

Se implementó un middleware centralizado para el manejo de excepciones utilizando:

- BusinessException
- NotFoundException

con respuestas HTTP consistentes.

## Pruebas

Se implementaron pruebas automatizadas utilizando:

- xUnit
- Moq

Incluyendo:

- Pruebas unitarias.
- Pruebas de endpoints.
- Prueba de integración.