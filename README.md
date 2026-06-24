# MedianetBank - Prueba Técnica

Sistema bancario basado en microservicios REST desarrollado en .NET 6, SQL Server y Entity Framework Core.

## Arquitectura

La solución está separada en dos microservicios:

- ClientePersona.Api: gestión de clientes y personas.
- CuentaMovimiento.Api: gestión de cuentas, movimientos y reportes.

Cada microservicio maneja su propia base de datos:

- ClientePersonaDb
- CuentaMovimientoDb

## Tecnologías

- .NET 6
- C#
- SQL Server
- Entity Framework Core
- Swagger / OpenAPI
- xUnit
- Moq

## Microservicios

### ClientePersona.Api

Responsable de:

- CRUD de clientes.
- Generación automática de ClienteId: CLI0001, CLI0002, CLI0003.
- Delete lógico mediante Estado = false.
- DTOs para evitar exponer contraseña.
- Manejo centralizado de excepciones.

### CuentaMovimiento.Api

Responsable de:

- CRUD de cuentas.
- Registro de movimientos.
- Validación de saldo disponible.
- Reportes de estado de cuenta.
- Reverso de movimientos para mantener trazabilidad bancaria.

## Comunicación entre microservicios

CuentaMovimiento.Api se comunica con ClientePersona.Api mediante HttpClient.

Esta comunicación se usa para:

- Validar que el cliente exista antes de crear una cuenta.
- Validar que el cliente exista antes de actualizar una cuenta.
- Obtener información del cliente para el reporte de estado de cuenta.

Ejemplo:

```http
GET https://localhost:7001/api/Clientes/codigo/CLI0001

Reglas de negocio
- Un cliente puede tener varias cuentas.
- El número de cuenta debe ser único.
- El saldo inicial no puede ser negativo.
- Un movimiento positivo representa depósito.
- Un movimiento negativo representa retiro.
- Si un retiro excede el saldo disponible, se retorna: "Saldo no disponible".
- Los movimientos históricos no se editan directamente; se usa reverso para mantener trazabilidad.

Endpoints principales
Clientes
	GET /api/Clientes
	GET /api/Clientes/{id}
	GET /api/Clientes/codigo/{clienteId}
	POST /api/Clientes
	PUT /api/Clientes/{id}
	DELETE /api/Clientes/{id}
Cuentas
	GET /api/Cuentas
	GET /api/Cuentas/{id}
	POST /api/Cuentas
	PUT /api/Cuentas/{id}
	DELETE /api/Cuentas/{id}
Movimientos
	GET /api/Movimientos
	GET /api/Movimientos/{id}
	POST /api/Movimientos
	POST /api/Movimientos/{id}/reverso
Reportes
	GET /api/Reportes?FechaInicio=2022-02-01&FechaFin=2022-02-28&ClienteId=CLI0002

Ejecución local
1. Restaurar paquetes: 
	dotnet restore

2. Aplicar migraciones de cada microservicio:
	Update-Database

3. Ejecutar ambos microservicios desde Visual Studio usando Multiple Startup Projects.

Puertos configurados:
- ClientePersona.Api: https://localhost:7001
- CuentaMovimiento.Api: https://localhost:7002

Swagger
Con ambos microservicios en ejecución:
- ClientePersona.Api: https://localhost:7001/swagger
- CuentaMovimiento.Api: https://localhost:7002/swagger

Pruebas
Se implementaron pruebas automatizadas con xUnit y Moq.
Total actual:
	5 pruebas correctas

Incluye:
- 2 pruebas unitarias sobre ClienteService.
- 2 pruebas de endpoints.
- 1 prueba de integración para validar saldo no disponible.

Ejecutar pruebas:
dotnet test

Base de datos
El script de base de datos se encuentra en:
- BaseDatos.sql

Incluye:
- Esquema.
- Entidades.
- Datos de ejemplo del caso de uso.

Uso de IA
La carpeta ai/ contiene evidencia del uso de Inteligencia Artificial durante el desarrollo:
	ai/
	├── prompts.md
	├── decisions.md
	├── generations/
	└── agent/

La IA fue utilizada para:
- Apoyar el diseño de arquitectura.
- Generar DTOs, services y repositories.
- Revisar validaciones de negocio.
- Generar pruebas unitarias.
- Documentar decisiones técnicas.
Todas las sugerencias fueron revisadas y ajustadas manualmente antes de integrarse al proyecto.

Decisiones de diseño
- Se separó la solución en dos microservicios para respetar la responsabilidad de cada dominio.
- Se usó ClienteId de negocio como identificador entre microservicios.
- Se implementó delete lógico para preservar información bancaria.
- No se editan movimientos históricos; se implementó reverso para trazabilidad.
- Se aplicó manejo centralizado de excepciones para respuestas consistentes.

Pendientes / mejoras futuras
- Frontend Angular.
- Docker y docker-compose.
- JMeter para prueba de carga.
- Mejorar cobertura de pruebas.