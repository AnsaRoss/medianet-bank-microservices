# Agente de Revision Asistida

Este agente funciona como un asistente local de checklist para la prueba tecnica. Revisa la estructura del repositorio y genera un reporte con evidencias encontradas, riesgos y pendientes contra los entregables solicitados.

## Objetivo

- Reducir omisiones antes de la entrega.
- Revisar evidencias de microservicios, frontend, Docker, pruebas, Postman, JMeter e IA.
- Detectar pendientes frecuentes como carpetas vacias, scripts con nombre distinto al requerido o falta de validaciones declarativas.

## Requisitos

- Python 3.8 o superior.
- No requiere paquetes externos.

## Ejecucion

Desde la raiz del repositorio:

```bash
python ai/agent/checklist_agent.py
```

En Windows tambien se puede ejecutar sin depender de Python:

```powershell
powershell -ExecutionPolicy Bypass -File ai/agent/checklist_agent.ps1
```

El agente genera el archivo:

```text
ai/agent/checklist-report.md
```

## Solucion de problemas

Si `python ai/agent/checklist_agent.py` muestra un error relacionado con MySQL Workbench o `No module named 'encodings'`, significa que el comando `python` esta apuntando al Python embebido de MySQL Workbench. En ese caso use la version PowerShell del agente:

```powershell
powershell -ExecutionPolicy Bypass -File ai/agent/checklist_agent.ps1
```

## Como se uso

El agente se utilizo como apoyo de validacion antes de la entrega. Sus hallazgos fueron revisados manualmente para decidir que corregir, que documentar y que justificar tecnicamente.

## Limitaciones

- No reemplaza la ejecucion real de pruebas ni Docker.
- No interpreta reglas de negocio completas; revisa evidencias y patrones esperados.
- Los resultados deben ser validados por el desarrollador antes de tomar decisiones.
