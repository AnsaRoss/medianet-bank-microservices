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
## Agente de IA

Se decidio implementar un agente local de revision en lugar de integrar un endpoint con IA dentro del sistema bancario.

Motivos:

- La prueba exige evidenciar uso de IA aplicado al desarrollo, no necesariamente en produccion.
- Un agente de checklist ayuda a reducir omisiones de entrega.
- Es reproducible sin credenciales, API keys ni dependencias externas.

Se acepto:

- Generar un reporte Markdown con hallazgos verificables.
- Revisar estructura de carpetas, entregables y documentacion.
- Mantener recomendaciones como insumo para revision humana.

Se corrigio manualmente:

- El agente no decide automaticamente si el sistema cumple reglas de negocio.
- Los hallazgos quedan como OK, REVISION o PENDIENTE para que el desarrollador confirme.
- Se evitaron dependencias externas para facilitar ejecucion local.

## Generaciones IA Conservadas

Se agregaron fragmentos representativos en ai/generations/:

- backend-validations.md: propuesta de validaciones de entrada para DTOs.
- testing-and-docs.md: propuesta de pruebas y documentacion.

Estos fragmentos no se copiaron ciegamente. Fueron usados como base y revisados contra la estructura real del proyecto.
