<p align="center">
  <img src="assets/readme/hero.svg" alt="SiGeP - Sistema de Gestión de Pacientes: arquitectura en capas sobre .NET 8" width="100%">
</p>

<p align="center">
  <img alt=".NET 8" src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white">
  <img alt="Blazor Server" src="https://img.shields.io/badge/UI-Blazor%20Server-5C2D91?logo=blazor&logoColor=white">
  <img alt="EF Core 8" src="https://img.shields.io/badge/ORM-EF%20Core%208-0e8f86">
  <img alt="SQL Server" src="https://img.shields.io/badge/DB-SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white">
  <img alt="JWT" src="https://img.shields.io/badge/Auth-JWT-000000?logo=jsonwebtokens&logoColor=white">
  <img alt="Docker" src="https://img.shields.io/badge/Docker-ready-2496ED?logo=docker&logoColor=white">
</p>

<p align="center">
  <b>Gestión de pacientes, médicos, turnos, historias clínicas y pagos</b><br>
  API REST con JWT + frontend Blazor Server, en una solución por capas sobre .NET 8.
</p>

---

## ✨ Qué es

**SiGeP** es una aplicación para administrar el día a día de un consultorio o clínica: alta de **pacientes** y **médicos**, agenda de **turnos**, **historia clínica**, **pagos** y **recordatorios**. Está pensada como base de referencia de arquitectura n-tier en .NET 8 (Repository + Unit of Work, Observer, DTOs con AutoMapper).

| | |
|---|---|
| 🧱 **Capas claras** | `Model → DataAccess → Business → API → UI`, dependencias siempre hacia abajo. |
| 🔐 **Autenticación JWT** | Login contra la API; la UI guarda los claims en una cookie de sesión. |
| 🗃️ **Soft delete + auditoría** | Toda entidad hereda `BaseEntity` (Id, creado/modificado/borrado) y se filtra de forma global. |
| 🔔 **Observer** | Notificaciones de pagos y recordatorios de turnos desacopladas de la lógica de negocio. |
| 📜 **Logging** | Serilog con sink a SQL Server (`DatabaseLogsSiGeP`). |

## 🗺️ Arquitectura

```mermaid
flowchart LR
    M[SiGeP.Model<br/>Entidades · DTOs · DbContext] --> D[SiGeP.DataAccess<br/>Repository · UnitOfWork]
    D --> B[SiGeP.Business<br/>Reglas · Observer]
    B --> A[SiGeP.API<br/>REST · JWT · AutoMapper · Serilog]
    A --> U[SiGeP.UI<br/>Blazor Server · Syncfusion · Identity]
```

| Proyecto | Responsabilidad |
|---|---|
| `SiGeP.Model` | Entidades de dominio (`Customer`, `Doctor`, `Appointment`, `Address`, `MedicalRecord`, `Payment`, `Reminder`…), DTOs y `DbModelContext` (EF Core 8). |
| `SiGeP.DataAccess` | `GenericRepository<T>` (CRUD, paginación, SQL crudo) y `UnitOfWork`. Los repositorios se obtienen con `unitOfWork.AddRepositories.GetRepository<T>()`. |
| `SiGeP.Business` | Una clase `<Entidad>Business : BusinessBase<T>` por entidad, con validaciones y notificadores (Observer). |
| `SiGeP.API` | Controllers REST, JWT, AutoMapper, middleware de excepciones y Swagger. |
| `SiGeP.UI` | Frontend Blazor Server con Syncfusion, Identity (login/cookie) y FluentValidation. |

## 🚀 Inicio rápido

**Requisitos:** SDK de .NET 8, SQL Server (y SSMS opcional).

```bash
# 1. Clonar
git clone <url-del-repo>
cd SiGeP

# 2. Crear las bases DatabaseSiGeP y DatabaseLogsSiGeP en SQL Server
#    y ajustar los connection strings (preferible con user-secrets)
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<cadena>" --project SiGeP.API

# 3. Aplicar migraciones
dotnet ef database update --project SiGeP.Model --startup-project SiGeP.API --context DbModelContext

# 4. Levantar API (Swagger + JWT) y luego la UI
dotnet run --project SiGeP.API
dotnet run --project SiGeP.UI
```

> [!NOTE]
> Los nombres exactos de las claves de configuración están en `SiGeP.API/appsettings.json`. La UI requiere la API en ejecución.

<details>
<summary><b>Migraciones EF Core</b></summary>

```bash
dotnet ef migrations add <Nombre> --project SiGeP.Model --startup-project SiGeP.API --context DbModelContext
dotnet ef database update          --project SiGeP.Model --startup-project SiGeP.API --context DbModelContext
```
</details>


## 📚 Documentación

| Documento | Contenido |
|---|---|
| [Arquitectura](SiGeP/Guia%20Desarrollo/ReadmeArquitecture.md) | Relevamiento detallado: patrones, seguridad, convenciones y plantillas por capa. |
| [Estructura](SiGeP/Guia%20Desarrollo/Estructura.md) | Árbol de carpetas comentado de los 5 proyectos. |
| [Bitácora API](SiGeP/Documentacion/Readme.md) | Pasos de alta: EF Core, UnitOfWork, AutoMapper, JWT, Serilog. |
| [Bitácora UI](SiGeP/Documentacion/Readme.UI.md) | Blazor Server, Syncfusion, Identity, FluentValidation. |
| [Angular (idea)](SiGeP/Documentacion/Angular.md) | Notas sobre un frontend alternativo, no implementado. |
