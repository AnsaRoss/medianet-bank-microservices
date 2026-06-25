$RootPath = Resolve-Path (Join-Path $PSScriptRoot "..\..")
$ReportPath = Join-Path $PSScriptRoot "checklist-report.md"

function Test-FileExists {
    param([string]$RelativePath)
    Test-Path (Join-Path $RootPath $RelativePath)
}

function Test-FolderHasFiles {
    param([string]$RelativePath)
    $folder = Join-Path $RootPath $RelativePath
    if (-not (Test-Path $folder)) {
        return $false
    }

    $files = Get-ChildItem -Path $folder -File | Where-Object { $_.Name -ne ".gitkeep" }
    return ($files.Count -gt 0)
}

function Read-RepoText {
    param([string]$RelativePath)
    $file = Join-Path $RootPath $RelativePath
    if (-not (Test-Path $file)) {
        return ""
    }

    return Get-Content -Path $file -Raw -ErrorAction SilentlyContinue
}

function Get-Status {
    param(
        [bool]$Condition,
        [bool]$WarningCondition = $false
    )

    if ($Condition) {
        return "OK"
    }

    if ($WarningCondition) {
        return "REVISION"
    }

    return "PENDIENTE"
}

$readme = (Read-RepoText "README.md").ToLowerInvariant()
$packageJson = Read-RepoText "medianet-bank-frontend\package.json"
$dtoFiles = @()

foreach ($folder in @("ClientePersona.Api\DTOs", "CuentaMovimiento.Api\DTOs")) {
    $path = Join-Path $RootPath $folder
    if (Test-Path $path) {
        $dtoFiles += Get-ChildItem -Path $path -Filter "*.cs"
    }
}

$postmanExists = (Get-ChildItem -Path (Join-Path $RootPath "Postman") -Filter "*.json" -ErrorAction SilentlyContinue).Count -gt 0
$coverageExists = Test-FileExists "CoverageReport\index.html"
$jmeterExists = (Test-FileExists "Evidencia_JMeter.docx") -or ((Get-ChildItem -Path $RootPath -Filter "*.jmx" -ErrorAction SilentlyContinue).Count -gt 0)
$hasValidations = $false

if ($dtoFiles.Count -gt 0) {
    $validationMatches = Select-String -Path $dtoFiles.FullName -Pattern "\[Required\]|\[Range|\[StringLength|\[MaxLength" -ErrorAction SilentlyContinue
    $hasValidations = $null -ne $validationMatches
}
$readmeComplete = $readme.Contains("arquitectura") -and $readme.Contains("docker") -and $readme.Contains("microservicios") -and $readme.Contains("ia")

$checks = @(
    [pscustomobject]@{
        Name = "Microservicio Cliente/Persona"
        Status = Get-Status (Test-FileExists "ClientePersona.Api\ClientePersona.Api.csproj")
        Evidence = "Se encontro ClientePersona.Api con proyecto .NET."
        Recommendation = "Mantener Swagger y README alineados con sus endpoints."
    },
    [pscustomobject]@{
        Name = "Microservicio Cuenta/Movimientos"
        Status = Get-Status (Test-FileExists "CuentaMovimiento.Api\CuentaMovimiento.Api.csproj")
        Evidence = "Se encontro CuentaMovimiento.Api con proyecto .NET."
        Recommendation = "Verificar CRUD de cuentas, movimientos y reportes."
    },
    [pscustomobject]@{
        Name = "Frontend Angular"
        Status = Get-Status (Test-FileExists "medianet-bank-frontend\package.json")
        Evidence = "Se encontro package.json del frontend."
        Recommendation = "Ejecutar build del frontend antes de entregar."
    },
    [pscustomobject]@{
        Name = "Angular Material y Bootstrap"
        Status = Get-Status (($packageJson.Contains("@angular/material")) -and ($packageJson.Contains('"bootstrap": "5.2.0"'))) (($packageJson.Contains("@angular/material")) -and ($packageJson.Contains("bootstrap")))
        Evidence = "Se revisaron dependencias declaradas en package.json."
        Recommendation = "Confirmar version exacta de Bootstrap solicitada por el enunciado."
    },
    [pscustomobject]@{
        Name = "Docker Compose"
        Status = Get-Status (Test-FileExists "docker-compose.yml")
        Evidence = "Se encontro docker-compose.yml."
        Recommendation = "Ejecutar docker compose config/build/up y documentar resultado."
    },
    [pscustomobject]@{
        Name = "Postman"
        Status = Get-Status $postmanExists
        Evidence = "Se encontro coleccion Postman exportada."
        Recommendation = "Validar que incluya casos felices y errores de saldo."
    },
    [pscustomobject]@{
        Name = "JMeter"
        Status = Get-Status $jmeterExists
        Evidence = "Se encontro evidencia JMeter o plan de prueba."
        Recommendation = "Incluir endpoint probado, concurrencia, resultado y captura."
    },
    [pscustomobject]@{
        Name = "Cobertura"
        Status = Get-Status $coverageExists
        Evidence = "Se encontro reporte HTML de cobertura."
        Recommendation = "Indicar comando usado para generar cobertura en README."
    },
    [pscustomobject]@{
        Name = "Script BaseDatos.sql"
        Status = Get-Status (Test-FileExists "BaseDatos.sql") (Test-FileExists "BDScripts")
        Evidence = "Se reviso si existe el nombre exacto solicitado por el PDF."
        Recommendation = "Crear o documentar un script consolidado BaseDatos.sql si se mantiene separado por microservicio."
    },
    [pscustomobject]@{
        Name = "Evidencia IA - prompts"
        Status = Get-Status (Test-FileExists "ai\prompts.md")
        Evidence = "Se encontro ai/prompts.md."
        Recommendation = "Mantener prompts agrupados por objetivo."
    },
    [pscustomobject]@{
        Name = "Evidencia IA - generaciones"
        Status = Get-Status (Test-FolderHasFiles "ai\generations")
        Evidence = "Se reviso si ai/generations contiene archivos distintos de .gitkeep."
        Recommendation = "Guardar fragmentos representativos generados y revisados."
    },
    [pscustomobject]@{
        Name = "Evidencia IA - agente"
        Status = Get-Status (Test-FolderHasFiles "ai\agent")
        Evidence = "Se reviso si ai/agent contiene codigo o instrucciones."
        Recommendation = "Incluir instrucciones de ejecucion y limitaciones."
    },
    [pscustomobject]@{
        Name = "Validaciones de entrada"
        Status = Get-Status $hasValidations $true
        Evidence = "Se buscaron DataAnnotations en DTOs de entrada."
        Recommendation = "Agregar validaciones declarativas o documentar validaciones manuales en servicios."
    },
    [pscustomobject]@{
        Name = "README requerido"
        Status = Get-Status $readmeComplete (Test-FileExists "README.md")
        Evidence = "Se revisaron secciones basicas del README."
        Recommendation = "Completar pasos de ejecucion, comunicacion, IA y consideraciones no funcionales."
    }
)

$ok = @($checks | Where-Object { $_.Status -eq "OK" }).Count
$revision = @($checks | Where-Object { $_.Status -eq "REVISION" }).Count
$pending = @($checks | Where-Object { $_.Status -eq "PENDIENTE" }).Count

$lines = @(
    "# Reporte del Agente de Revision",
    "",
    "Fecha de generacion: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')",
    "",
    "## Resumen",
    "",
    "- Total de checks: $($checks.Count)",
    "- OK: $ok",
    "- Revision: $revision",
    "- Pendiente: $pending",
    "",
    "## Detalle",
    ""
)

foreach ($check in $checks) {
    $lines += @(
        "### $($check.Name)",
        "",
        "- Estado: $($check.Status)",
        "- Evidencia: $($check.Evidence)",
        "- Recomendacion: $($check.Recommendation)",
        ""
    )
}

$lines += @(
    "## Uso esperado",
    "",
    "Este reporte se usa como insumo de revision. Cada hallazgo debe confirmarse manualmente antes de ajustar codigo o documentacion.",
    ""
)

Set-Content -Path $ReportPath -Value $lines -Encoding UTF8
Write-Host "Reporte generado: $ReportPath"
