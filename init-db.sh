#!/bin/bash

echo "Esperando SQL Server..."
sleep 30

echo "Ejecutando scripts de base de datos..."

echo "ClientePersonaDb..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "Your_password123" -i /scripts/ClientePersonaDb.sql

echo "CuentaMovimientoDb..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "Your_password123" -i /scripts/CuentaMovimientoDb.sql

echo "Bases de datos inicializadas correctamente."