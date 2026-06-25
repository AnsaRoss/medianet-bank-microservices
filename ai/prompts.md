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
## Agente de Revision

Prompt utilizado:

> Actua como un asistente de revision para una prueba tecnica bancaria. Compara el repositorio contra los entregables solicitados: microservicios, frontend Angular, Docker, Postman, Swagger, pruebas, cobertura, JMeter y carpeta ai/. Genera una checklist con estado, evidencia y recomendacion. No modifiques codigo; solo reporta hallazgos verificables.

Resultado aplicado:

- Se construyo un agente local en ai/agent/checklist_agent.py.
- El agente revisa archivos y carpetas esperadas.
- El agente genera ai/agent/checklist-report.md para detectar pendientes antes de la entrega.

## Validacion de Entrada

Prompt utilizado:

> Revisa DTOs de entrada de una API bancaria en .NET 6 y propone validaciones con DataAnnotations para Cliente, Cuenta, Movimiento y Reporte. Considera campos obligatorios, longitudes, rangos numericos y reglas bancarias basicas.

Resultado aplicado:

- Se identifico la necesidad de agregar validaciones declarativas en DTOs.
- Se mantuvo la validacion de reglas de negocio en servicios para proteger el dominio.
- Se documento el pendiente en ai/generations/backend-validations.md.
