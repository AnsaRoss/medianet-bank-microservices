/*
    Script consolidado de base de datos para entrega.

    La solucion usa dos bases de datos independientes porque esta separada en
    dos microservicios:

    - ClientePersonaDb: dominio Cliente / Persona.
    - CuentaMovimientoDb: dominio Cuenta / Movimientos.

    Los scripts completos se mantienen en BDScripts/ para facilitar la
    inicializacion por docker-compose. Este archivo existe como punto de entrada
    unico solicitado por el enunciado de la prueba tecnica.

    Ejecucion con sqlcmd desde la raiz del repositorio:

    sqlcmd -S localhost -U sa -P "Your_password123" -C -i BaseDatos.sql
*/

:r BDScripts\ClientePersonaDb.sql
:r BDScripts\CuentaMovimientoDb.sql
