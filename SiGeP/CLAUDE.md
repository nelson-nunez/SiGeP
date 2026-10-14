# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Documento de arquitectura de referencia

Antes de encarar cualquier tarea no trivial, leer **`Guia Desarrollo/ReadmeArquitectura.md`** — es un
relevamiento vivo y detallado del código real (estructura de carpetas, patrones, seguridad, convenciones,
plantillas de clases por capa, checklist para agregar una entidad end-to-end). Este `CLAUDE.md` resume solo lo
esencial para orientarse rápido; ese documento tiene el detalle y los `TODO: revisar` con deuda técnica real.

## Comandos

La solución no tiene test project ni scripts de build/lint propios (no hay `*.Tests.csproj`, ni linter
configurado más allá del default de Visual Studio). Se compila y corre con el SDK de .NET 8 estándar:

```
dotnet build SiGeP.sln

dotnet run --project SiGeP.API     # API (Swagger + JWT)
dotnet run --project SiGeP.UI      # Blazor Server (requiere la API corriendo)
```

Migraciones EF Core (proyecto de modelo `SiGeP.Model`, proyecto de arranque `SiGeP.API`, contexto `DbModelContext`):

```
dotnet ef migrations add <Nombre> --project SiGeP.Model --startup-project SiGeP.API --context DbModelContext
dotnet ef database update --project SiGeP.Model --startup-project SiGeP.API --context DbModelContext
```

Requiere SQL Server con dos bases: `DatabaseSiGeP` (datos) y `DatabaseLogsSiGeP` (logs de Serilog). Strings de
conexión en `SiGeP.API/appsettings.json` (ya versionados en git en texto plano — ver sección de seguridad).
Ambos `.csproj` (`SiGeP.API`, `SiGeP.UI`) ya tienen `UserSecretsId` configurado: preferir
`dotnet user-secrets set` para credenciales nuevas en vez de tocar `appsettings.json`.

## Arquitectura — resumen

Solución en capas (n-tier), 5 proyectos, todos `net8.0`:

```
SiGeP.Model  →  SiGeP.DataAccess  →  SiGeP.Business  →  SiGeP.API  →  SiGeP.UI
```

`SiGeP.UI` (Blazor Server) referencia `SiGeP.API` directamente además de consumirla por HTTP, para reutilizar
sus clases comunes (`WebApiClient`, `JwtAuthResult`, `AppConfiguration`).

- **`SiGeP.Model`**: entidades (heredan `BaseEntity`: Id + auditoría + soft delete), DTOs, `DbModelContext` (EF
  Core 8, lazy loading vía proxies, query filter global de soft delete), migraciones.
- **`SiGeP.DataAccess`**: `GenericRepository<T>` con el CRUD/paginación/SQL crudo; los repositorios específicos
  hoy están vacíos. El acceso a un repositorio **no es por DI directa** sino por un Service Locator:
  `unitOfWork.AddRepositories.GetRepository<TEntity>()` (`AddRepositories.cs`, diccionario `Type → instancia`).
  `UnitOfWork.CompleteAsync()` centraliza auditoría y soft delete.
- **`SiGeP.Business`**: una clase `<Entidad>Business : BusinessBase<TEntidad>` por entidad; ahí vive también la
  lógica de consulta/validación específica (no hay capa de "servicios" adicional). Varias `SaveAsync` usan
  `SemaphoreSlim` estático para serializar altas/updates concurrentes de la misma entidad.
- **`SiGeP.API`**: controllers REST + AutoMapper (`Mapper/AutoMapperProfile.cs`) + JWT (`Common/IJwtAuthManager.cs`)
  + Serilog. DI de business en `ServiceExtension.AddBusinessServices`.
- **`SiGeP.UI`**: Blazor Server, frontend activo (hubo un intento en Angular, eliminado del repo). Cookie de
  sesión que encapsula los claims del JWT emitido por la API. Identity está scaffoldeado solo como shell de
  login/cookie, no como store real de usuarios (el login de negocio pasa por `AppUser`/`AuthenticationBusiness`).

Al agregar una entidad nueva end-to-end, seguir el checklist y las plantillas por capa de
`Guia Desarrollo/ReadmeArquitectura.md` (secciones 3 y 7–9) — cubre dónde va cada archivo nuevo (entidad, DTO,
repositorio, business, controller, página/componente Blazor) con ejemplos concretos.

## Puntos críticos a tener en cuenta (deuda técnica real, no hipotética)

- **Autorización deshabilitada**: los 5 controllers de negocio (`CustomerController`, `DoctorController`,
  `AddressController`, `GenderController`, `AppointmentController`) tienen `[Authorize]` comentado y heredan de
  `Controller` en vez de `BaseController`. Ningún endpoint de datos exige token hoy. Si se toca un endpoint de
  estos, considerar corregirlo (heredar `BaseController`, descomentar `[Authorize]`) salvo que se indique lo
  contrario.
- **Contraseñas de `AppUser` sin hash** (comparación en texto plano en `AuthenticationBusiness.Authenticate`).
- **El JWT completo viaja como query string** en el flujo de login UI (`Login.cshtml.cs`).
- **Credenciales de SQL Server y `JwtTokenConfig:Secret` en texto plano**, ya commiteados en
  `SiGeP.API/appsettings.json` pese a estar en `.gitignore` (agregado después del commit inicial, no tiene efecto
  retroactivo).
- **Sin proyecto de tests automatizados** en la solución.
- Manejo de excepciones inconsistente entre controllers (no hay middleware centralizado); confirmar antes de
  asumir que Serilog está escribiendo a `DatabaseLogsSiGeP` (la inicialización del logger está comentada en
  `SiGeP.API/Program.cs`).
- `ABMComponent.razor` y `CustomerCRUD.razor` (Blazor) no son funcionales todavía como CRUD de pacientes — no
  asumir que existe un componente CRUD reutilizable.

Ver `Guia Desarrollo/ReadmeArquitectura.md` sección 5 y 9 para el detalle completo y la checklist de seguridad
antes de tocar `appsettings.json` o cualquier endpoint.
