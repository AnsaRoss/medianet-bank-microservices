# MedianetBank - Prueba Tecnica

Sistema bancario basado en microservicios REST desarrollado con .NET 6, SQL Server, Entity Framework Core y frontend Angular 13.

## Arquitectura

La solucion esta separada en dos microservicios:

- `ClientePersona.Api`: gestion de clientes y personas.
- `CuentaMovimiento.Api`: gestion de cuentas, movimientos y reportes.

Cada microservicio maneja su propia base de datos:

- `ClientePersonaDb`
- `CuentaMovimientoDb`

La comunicacion entre microservicios se realiza mediante `HttpClient`. `CuentaMovimiento.Api` consulta a `ClientePersona.Api` para validar clientes y obtener datos del cliente en reportes.

## Tecnologias

- .NET 6
- C#
- SQL Server
- Entity Framework Core
- Swagger / OpenAPI
- xUnit
- Moq
- Angular 13
- Angular Material 13
- Bootstrap 5.2.0
- Docker
- Docker Compose
- Postman
- JMeter

## Microservicios

### ClientePersona.Api

Responsable de:

- CRUD de clientes.
- Generacion automatica de `ClienteId`: `CLI0001`, `CLI0002`, etc.
- Delete logico mediante `Estado = false`.
- Validaciones de entrada con DataAnnotations.
- DTOs para evitar exponer contrasena.
- Manejo centralizado de excepciones.

Validaciones principales:

- Identificacion: 10 digitos.
- Telefono: solo numeros, entre 7 y 15 digitos.
- Genero: `Masculino`, `Femenino` u `Otro`.
- Edad: entre 18 y 120.
- Bloqueo de valores genericos como `string`, `strings`, `test` o `prueba`.

### CuentaMovimiento.Api

Responsable de:

- CRUD de cuentas.
- CRUD auditable de movimientos.
- Registro de depositos y retiros.
- Validacion de saldo disponible.
- Reportes de estado de cuenta.

Validaciones principales:

- Numero de cuenta: solo numeros, maximo 10 digitos.
- Tipo de cuenta: `Ahorro`, `Ahorros` o `Corriente`.
- ClienteId: formato `CLI0001`.
- Saldo inicial no negativo.

## Reglas de Negocio

- Un cliente puede tener varias cuentas.
- El numero de cuenta debe ser unico.
- El saldo inicial no puede ser negativo.
- Un movimiento positivo representa deposito.
- Un movimiento negativo representa retiro.
- Si un retiro excede el saldo disponible, se retorna: `Saldo no disponible`.
- Los movimientos historicos no se eliminan fisicamente.
- Para correcciones se genera un reverso del movimiento original y luego un nuevo movimiento corregido.
- `DELETE /api/Movimientos/{id}` funciona como reverso auditable.

## Endpoints Principales

### Clientes

```text
GET    /api/Clientes
GET    /api/Clientes/{id}
GET    /api/Clientes/codigo/{clienteId}
POST   /api/Clientes
PUT    /api/Clientes/{id}
DELETE /api/Clientes/{id}
```

### Cuentas

```text
GET    /api/Cuentas
GET    /api/Cuentas/{id}
POST   /api/Cuentas
PUT    /api/Cuentas/{id}
DELETE /api/Cuentas/{id}
```

### Movimientos

```text
GET    /api/Movimientos
GET    /api/Movimientos/{id}
POST   /api/Movimientos
PUT    /api/Movimientos/{id}
DELETE /api/Movimientos/{id}
POST   /api/Movimientos/{id}/reverso
```

### Reportes

```text
GET /api/Reportes?FechaInicio=2022-02-01&FechaFin=2022-02-28&ClienteId=CLI0002
```

## Ejecucion Local

1. Restaurar paquetes:

```bash
dotnet restore
```

2. Configurar las cadenas de conexion en cada `appsettings.json`.

3. Aplicar migraciones de cada microservicio desde Visual Studio o Package Manager Console:

```powershell
Update-Database
```

4. Ejecutar ambos microservicios desde Visual Studio usando Multiple Startup Projects.

Puertos configurados:

- ClientePersona.Api: `https://localhost:7001`
- CuentaMovimiento.Api: `https://localhost:7002`

## Swagger

Con ambos microservicios en ejecucion:

- ClientePersona.Api: `https://localhost:7001/swagger`
- CuentaMovimiento.Api: `https://localhost:7002/swagger`

## Frontend Angular

El frontend se encuentra en:

```text
medianet-bank-frontend
```

Para ejecutarlo:

```bash
cd medianet-bank-frontend
npm install
npm start
```

El frontend consume las APIs configuradas en:

- `medianet-bank-frontend/src/environments/environment.ts`
- `medianet-bank-frontend/src/environments/environment.prod.ts`

Pantallas incluidas:

- Clientes
- Cuentas
- Movimientos
- Reportes

## Docker

La solucion incluye configuracion para ejecucion mediante Docker:

- `ClientePersona.Api/Dockerfile`
- `CuentaMovimiento.Api/Dockerfile`
- `medianet-bank-frontend/Dockerfile`
- `docker-compose.yml`
- `init-db.sh`

Comandos:

```bash
docker compose config
docker compose build
docker compose up
```

Servicios configurados:

- SQL Server: `localhost:1433`
- ClientePersona.Api: `http://localhost:7001`
- CuentaMovimiento.Api: `http://localhost:7002`
- Frontend: `http://localhost:4200`

El contenedor `db-init` ejecuta:

- `BDScripts/ClientePersonaDb.sql`
- `BDScripts/CuentaMovimientoDb.sql`

## Base de Datos

El entregable incluye `BaseDatos.sql` como punto de entrada solicitado por el enunciado.

La arquitectura mantiene dos bases independientes por microservicio:

- `BDScripts/ClientePersonaDb.sql`
- `BDScripts/CuentaMovimientoDb.sql`

`BaseDatos.sql` referencia ambos scripts para mantener compatibilidad con el entregable formal sin cambiar la separacion por microservicio.

## Pruebas

Se implementaron pruebas automatizadas con xUnit y Moq.

Incluye:

- Pruebas unitarias sobre `ClienteService`.
- Pruebas de endpoints.
- Prueba de integracion para validar saldo no disponible.

Ejecutar pruebas:

```bash
dotnet test
```

Resultado validado:

```text
5 pruebas exitosas
```

## Cobertura

El reporte de cobertura se encuentra en:

```text
CoverageReport/index.html
```

## Postman

La coleccion Postman se encuentra en:

```text
Postman/_MediTest.postman_collection.json
```

Incluye endpoints principales de clientes, cuentas, movimientos y reportes.

## JMeter

La evidencia de prueba de carga se encuentra en:

```text
Evidencia_JMeter.docx
```

La prueba se enfoca en el endpoint critico de registro de movimientos.

## Uso de IA

La carpeta `ai/` contiene evidencia del uso de Inteligencia Artificial durante el desarrollo:

```text
ai/
  prompts.md
  decisions.md
  generations/
  agent/
```

La IA fue utilizada para:

- Apoyar el diseno de arquitectura.
- Proponer DTOs, servicios y repositorios.
- Revisar validaciones de negocio.
- Generar ideas de pruebas unitarias e integracion.
- Documentar decisiones tecnicas.
- Construir un agente local de revision de entregables.

### Agente IA

El agente local revisa el repositorio contra una checklist de entregables y genera:

```text
ai/agent/checklist-report.md
```

Ejecucion en Windows:

```powershell
powershell -ExecutionPolicy Bypass -File ai/agent/checklist_agent.ps1
```

Ejecucion opcional con Python:

```bash
python ai/agent/checklist_agent.py
```

Resultado actual del agente:

```text
14 OK
0 Revision
0 Pendiente
```

Las sugerencias de IA fueron revisadas manualmente antes de integrarse. Las decisiones aceptadas, corregidas y descartadas estan documentadas en:

- `ai/decisions.md`
- `ai/generations/`

## Decisiones de Diseno

- Se separo la solucion en dos microservicios para respetar la responsabilidad de cada dominio.
- Se uso `ClienteId` de negocio como identificador entre microservicios.
- Se implemento delete logico para clientes y cuentas.
- Los movimientos no se eliminan fisicamente; se reversan para preservar trazabilidad.
- Se implemento `PUT` de movimientos como correccion auditable: reverso del original y registro nuevo.
- Se aplico manejo centralizado de excepciones para respuestas consistentes.
- Se agregaron validaciones de entrada con DataAnnotations.
- Se agrego frontend Angular para validar el flujo funcional completo desde interfaz grafica.
- Se agrego Docker Compose para facilitar el despliegue local.

## Consideraciones No Funcionales

- Rendimiento: los endpoints usan consultas simples y DTOs; el endpoint critico de movimientos fue considerado para prueba de carga con JMeter.
- Escalabilidad: la separacion por microservicios permite escalar Cliente/Persona y Cuenta/Movimientos de forma independiente.
- Resiliencia: la comunicacion entre servicios se centraliza mediante `HttpClient`; como mejora futura se podria agregar retry, circuit breaker y timeouts explicitos.
- Seguridad: no se expone la contrasena en DTOs de respuesta; como mejora futura se podria agregar autenticacion, autorizacion y hash de contrasenas.
- Observabilidad: como mejora futura se podria incorporar logging estructurado, metricas y trazas distribuidas.

## Mejoras Futuras

- Incrementar cobertura de pruebas automatizadas.
- Agregar politicas de resiliencia entre microservicios.
- Implementar autenticacion y autorizacion.
- Incorporar monitoreo y observabilidad.
- Automatizar pipeline de build/test.
