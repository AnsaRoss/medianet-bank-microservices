# Reporte del Agente de Revision

Fecha de generacion: 2026-06-25 13:01:19

## Resumen

- Total de checks: 14
- OK: 14
- Revision: 0
- Pendiente: 0

## Detalle

### Microservicio Cliente/Persona

- Estado: OK
- Evidencia: Se encontro ClientePersona.Api con proyecto .NET.
- Recomendacion: Mantener Swagger y README alineados con sus endpoints.

### Microservicio Cuenta/Movimientos

- Estado: OK
- Evidencia: Se encontro CuentaMovimiento.Api con proyecto .NET.
- Recomendacion: Verificar CRUD de cuentas, movimientos y reportes.

### Frontend Angular

- Estado: OK
- Evidencia: Se encontro package.json del frontend.
- Recomendacion: Ejecutar build del frontend antes de entregar.

### Angular Material y Bootstrap

- Estado: OK
- Evidencia: Se revisaron dependencias declaradas en package.json.
- Recomendacion: Confirmar version exacta de Bootstrap solicitada por el enunciado.

### Docker Compose

- Estado: OK
- Evidencia: Se encontro docker-compose.yml.
- Recomendacion: Ejecutar docker compose config/build/up y documentar resultado.

### Postman

- Estado: OK
- Evidencia: Se encontro coleccion Postman exportada.
- Recomendacion: Validar que incluya casos felices y errores de saldo.

### JMeter

- Estado: OK
- Evidencia: Se encontro evidencia JMeter o plan de prueba.
- Recomendacion: Incluir endpoint probado, concurrencia, resultado y captura.

### Cobertura

- Estado: OK
- Evidencia: Se encontro reporte HTML de cobertura.
- Recomendacion: Indicar comando usado para generar cobertura en README.

### Script BaseDatos.sql

- Estado: OK
- Evidencia: Se reviso si existe el nombre exacto solicitado por el PDF.
- Recomendacion: Crear o documentar un script consolidado BaseDatos.sql si se mantiene separado por microservicio.

### Evidencia IA - prompts

- Estado: OK
- Evidencia: Se encontro ai/prompts.md.
- Recomendacion: Mantener prompts agrupados por objetivo.

### Evidencia IA - generaciones

- Estado: OK
- Evidencia: Se reviso si ai/generations contiene archivos distintos de .gitkeep.
- Recomendacion: Guardar fragmentos representativos generados y revisados.

### Evidencia IA - agente

- Estado: OK
- Evidencia: Se reviso si ai/agent contiene codigo o instrucciones.
- Recomendacion: Incluir instrucciones de ejecucion y limitaciones.

### Validaciones de entrada

- Estado: OK
- Evidencia: Se buscaron DataAnnotations en DTOs de entrada.
- Recomendacion: Agregar validaciones declarativas o documentar validaciones manuales en servicios.

### README requerido

- Estado: OK
- Evidencia: Se revisaron secciones basicas del README.
- Recomendacion: Completar pasos de ejecucion, comunicacion, IA y consideraciones no funcionales.

## Uso esperado

Este reporte se usa como insumo de revision. Cada hallazgo debe confirmarse manualmente antes de ajustar codigo o documentacion.

