from __future__ import annotations

from dataclasses import dataclass
from datetime import datetime
from pathlib import Path


ROOT = Path(__file__).resolve().parents[2]
REPORT_PATH = ROOT / "ai" / "agent" / "checklist-report.md"


@dataclass
class Check:
    name: str
    status: str
    evidence: str
    recommendation: str


def exists(path: str) -> bool:
    return (ROOT / path).exists()


def has_files(path: str) -> bool:
    folder = ROOT / path
    if not folder.exists():
        return False
    return any(item.is_file() and item.name != ".gitkeep" for item in folder.iterdir())


def read_text(path: str) -> str:
    file_path = ROOT / path
    if not file_path.exists():
        return ""
    return file_path.read_text(encoding="utf-8", errors="ignore")


def glob_any(pattern: str) -> bool:
    return any(ROOT.glob(pattern))


def check_status(condition: bool, warning_condition: bool = False) -> str:
    if condition:
        return "OK"
    if warning_condition:
        return "REVISION"
    return "PENDIENTE"


def run_checks() -> list[Check]:
    readme = read_text("README.md").lower()
    package_json = read_text("medianet-bank-frontend/package.json")
    dto_text = "\n".join(
        path.read_text(encoding="utf-8", errors="ignore")
        for folder in ["ClientePersona.Api/DTOs", "CuentaMovimiento.Api/DTOs"]
        for path in (ROOT / folder).glob("*.cs")
    )

    checks = [
        Check(
            "Microservicio Cliente/Persona",
            check_status(exists("ClientePersona.Api/ClientePersona.Api.csproj")),
            "Se encontro ClientePersona.Api con proyecto .NET.",
            "Mantener Swagger y README alineados con sus endpoints.",
        ),
        Check(
            "Microservicio Cuenta/Movimientos",
            check_status(exists("CuentaMovimiento.Api/CuentaMovimiento.Api.csproj")),
            "Se encontro CuentaMovimiento.Api con proyecto .NET.",
            "Verificar CRUD de cuentas, movimientos y reportes.",
        ),
        Check(
            "Frontend Angular",
            check_status(exists("medianet-bank-frontend/package.json")),
            "Se encontro package.json del frontend.",
            "Ejecutar build del frontend antes de entregar.",
        ),
        Check(
            "Angular Material y Bootstrap",
            check_status(
                "@angular/material" in package_json and '"bootstrap": "5.2.0"' in package_json,
                warning_condition="@angular/material" in package_json and "bootstrap" in package_json,
            ),
            "Se revisaron dependencias declaradas en package.json.",
            "Confirmar version exacta de Bootstrap solicitada por el enunciado.",
        ),
        Check(
            "Docker Compose",
            check_status(exists("docker-compose.yml")),
            "Se encontro docker-compose.yml.",
            "Ejecutar docker compose config/build/up y documentar resultado.",
        ),
        Check(
            "Postman",
            check_status(glob_any("Postman/*.json")),
            "Se encontro coleccion Postman exportada.",
            "Validar que incluya casos felices y errores de saldo.",
        ),
        Check(
            "JMeter",
            check_status(exists("Evidencia_JMeter.docx") or glob_any("*.jmx")),
            "Se encontro evidencia JMeter o plan de prueba.",
            "Incluir endpoint probado, concurrencia, resultado y captura.",
        ),
        Check(
            "Cobertura",
            check_status(exists("CoverageReport/index.html")),
            "Se encontro reporte HTML de cobertura.",
            "Indicar comando usado para generar cobertura en README.",
        ),
        Check(
            "Script BaseDatos.sql",
            check_status(exists("BaseDatos.sql"), warning_condition=exists("BDScripts")),
            "Se reviso si existe el nombre exacto solicitado por el PDF.",
            "Crear o documentar un script consolidado BaseDatos.sql si se mantiene separado por microservicio.",
        ),
        Check(
            "Evidencia IA - prompts",
            check_status(exists("ai/prompts.md")),
            "Se encontro ai/prompts.md.",
            "Mantener prompts agrupados por objetivo.",
        ),
        Check(
            "Evidencia IA - generaciones",
            check_status(has_files("ai/generations")),
            "Se reviso si ai/generations contiene archivos distintos de .gitkeep.",
            "Guardar fragmentos representativos generados y revisados.",
        ),
        Check(
            "Evidencia IA - agente",
            check_status(has_files("ai/agent")),
            "Se reviso si ai/agent contiene codigo o instrucciones.",
            "Incluir instrucciones de ejecucion y limitaciones.",
        ),
        Check(
            "Validaciones de entrada",
            check_status(
                any(token in dto_text for token in ["[Required]", "[Range]", "[StringLength]", "[MaxLength]"]),
                warning_condition=True,
            ),
            "Se buscaron DataAnnotations en DTOs de entrada.",
            "Agregar validaciones declarativas o documentar validaciones manuales en servicios.",
        ),
        Check(
            "README requerido",
            check_status(
                all(term in readme for term in ["arquitectura", "docker", "microservicios", "ia"]),
                warning_condition=exists("README.md"),
            ),
            "Se revisaron secciones basicas del README.",
            "Completar pasos de ejecucion, comunicacion, IA y consideraciones no funcionales.",
        ),
    ]

    return checks


def build_report(checks: list[Check]) -> str:
    total = len(checks)
    ok = sum(1 for check in checks if check.status == "OK")
    review = sum(1 for check in checks if check.status == "REVISION")
    pending = sum(1 for check in checks if check.status == "PENDIENTE")

    lines = [
        "# Reporte del Agente de Revision",
        "",
        f"Fecha de generacion: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}",
        "",
        "## Resumen",
        "",
        f"- Total de checks: {total}",
        f"- OK: {ok}",
        f"- Revision: {review}",
        f"- Pendiente: {pending}",
        "",
        "## Detalle",
        "",
    ]

    for check in checks:
        lines.extend(
            [
                f"### {check.name}",
                "",
                f"- Estado: {check.status}",
                f"- Evidencia: {check.evidence}",
                f"- Recomendacion: {check.recommendation}",
                "",
            ]
        )

    lines.extend(
        [
            "## Uso esperado",
            "",
            "Este reporte se usa como insumo de revision. Cada hallazgo debe confirmarse manualmente antes de ajustar codigo o documentacion.",
            "",
        ]
    )
    return "\n".join(lines)


def main() -> None:
    checks = run_checks()
    REPORT_PATH.write_text(build_report(checks), encoding="utf-8")
    print(f"Reporte generado: {REPORT_PATH}")


if __name__ == "__main__":
    main()
