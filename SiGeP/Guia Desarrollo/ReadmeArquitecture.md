# SiGeP - Arquitectura del Sistema

> Documento vivo. Basado en relevamiento real del código al **2026-09-01**. Antes de escribir un prompt o encarar
> una tarea de desarrollo nueva, leé al menos las secciones 3 (dónde va cada cosa), 5 (seguridad) y 9 (checklist).
> Todo lo marcado como **TODO: revisar** es una inconsistencia real detectada en el código actual, no una opinión.

## Tabla de contenidos

1. [Resumen general del proyecto](#1-resumen-general-del-proyecto)
2. [Stack tecnológico](#2-stack-tecnológico)
3. [Estructura de carpetas — dónde ubicar cada archivo nuevo](#3-estructura-de-carpetas--dónde-ubicar-cada-archivo-nuevo)
4. [Arquitectura, paradigmas y patrones de diseño](#4-arquitectura-paradigmas-y-patrones-de-diseño)
5. [Seguridad: autenticación y autorización](#5-seguridad-autenticación-y-autorización)
6. [Convenciones de código y estilo](#6-convenciones-de-código-y-estilo)
7. [Plantillas de clases por tipo](#7-plantillas-de-clases-por-tipo)
8. [Frontend: pantallas, componentes, estado](#8-frontend-pantallas-componentes-estado)
9. [Guía de uso y checklist rápido](#9-guía-de-uso-y-checklist-rápido)

---

## 1. Resumen general del proyecto

SiGeP (Sistema de Gestión de Pacientes) es una aplicación **.NET 8.0** para gestión de pacientes, médicos, turnos
y domicilios. La solución (`SiGeP.sln`) tiene 5 proyectos de código:

`SiGeP.Model` → `SiGeP.DataAccess` → `SiGeP.Business` → `SiGeP.API` → `SiGeP.UI`

con una capa adicional: **`SiGeP.UI` referencia el proyecto `SiGeP.API` directamente** (no solo lo consume por
HTTP), para reutilizar sus clases comunes (`WebApiClient`, `JwtAuthResult`, `AppConfiguration`, etc. — ver
sección 4.4). Esto es una particularidad real del proyecto, no el layering clásico "UI solo habla HTTP con la API".

El backend (`SiGeP.API`) expone una API REST consumida por dos frentes:
- **`SiGeP.UI`**: Blazor Server, es el frontend activo y en desarrollo.
- Hubo un intento de frontend en **Angular** (`SiGeP.UI.Angular`, commit `6f3fd19`, abril 2025) que fue
  posteriormente eliminado del repo. Solo queda `Documentacion/Angular.txt` con notas de instalación. **No forma
  parte del código actual.**

Estado general: el dominio (Model/DataAccess/Business) está relativamente maduro para las entidades de
Paciente/Médico/Turno/Domicilio. La capa de seguridad (autorización de endpoints, hash de contraseñas) y la UI de
Blazor (CRUD de pacientes) están **incompletas / en construcción activa** — ver secciones 5 y 8 para el detalle
exacto, no asumir que lo documentado en versiones previas de este archivo sigue vigente (no es así: ver nota al
final de la sección 4).

## 2. Stack tecnológico

Relevado desde los `.csproj` de cada proyecto (2026-09-01).

| Proyecto | SDK | Target |
|---|---|---|
| SiGeP.Model | Microsoft.NET.Sdk | net8.0 |
| SiGeP.DataAccess | Microsoft.NET.Sdk | net8.0 |
| SiGeP.Business | Microsoft.NET.Sdk | net8.0 |
| SiGeP.API | Microsoft.NET.Sdk.Web | net8.0 |
| SiGeP.UI | Microsoft.NET.Sdk.Web | net8.0 |

**SiGeP.Model**
- `Microsoft.EntityFrameworkCore.SqlServer` 8.0.8
- `Microsoft.EntityFrameworkCore.Proxies` 8.0.8 (lazy loading)
- `Microsoft.EntityFrameworkCore.Tools` / `.Design` 8.0.8
- `Microsoft.Extensions.Configuration` / `.Json` 8.0.0
- `LinqKit` 1.3.0 (construcción de predicados dinámicos, usado en `EntityFrameworkExtension`)

**SiGeP.DataAccess**
- `Microsoft.AspNetCore.Http.Abstractions` 2.1.1 (única dependencia directa; el resto viene transitivamente de Model)

**SiGeP.Business**
- Sin paquetes NuGet propios; solo referencia a DataAccess y Model.

**SiGeP.API**
- `AutoMapper` 13.0.1
- `Microsoft.AspNetCore.Authentication.JwtBearer` 8.0.8
- `Microsoft.EntityFrameworkCore.Design` 8.0.8
- `Serilog.AspNetCore` 8.0.2, `Serilog.Sinks.MSSqlServer` 6.6.1
- `Swashbuckle.AspNetCore` 6.4.0 (Swagger/OpenAPI)
- `Microsoft.VisualStudio.Azure.Containers.Tools.Targets` 1.20.1 (soporte Docker)

**SiGeP.UI**
- `FluentValidation` 11.9.2 (validación en formularios Blazor)
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` / `.UI` 8.0.8 (Identity scaffolded, solo para las páginas de
  cuenta — ver sección 5)
- `Microsoft.EntityFrameworkCore.SqlServer` / `.Tools` 8.0.8
- `Syncfusion.Blazor.*` (Buttons/DropDowns/Inputs/Popups en 27.1.50, **Grid en 19.2.0.55** — versión desalineada
  del resto de los paquetes Syncfusion, **TODO: revisar**)
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` 8.0.4 (scaffolding de Identity)

**Base de datos**: SQL Server (dos bases: `DatabaseSiGeP` para datos, `DatabaseLogsSiGeP` para logs de Serilog),
acceso vía EF Core 8 Code-First con migraciones (`SiGeP.Model/Migrations`, una única migración
`20260901210602_InitialCreate` al momento de este relevamiento).

**Testing**: no se encontró ningún proyecto de test en la solución (`*.Tests.csproj`) ni carpeta de tests. El
commit `1843916 "Agregados test basicos"` existe en el historial pero no hay artefactos de test en el estado
actual del repo. **TODO: revisar** — no hay cobertura de tests automatizados hoy.

**Frontend alternativo (histórico, no activo)**: Angular (ver sección 1).

## 3. Estructura de carpetas — dónde ubicar cada archivo nuevo

Raíz de la solución (`SiGeP/`):

```
SiGeP/
├── SiGeP.sln
├── Documentacion/          Documentos de referencia general (Readme.txt, Readme.UI.txt, Angular.txt, diagrama de clases)
├── Guia Desarrollo/        Este archivo + Estructura.txt (mapa de carpetas/archivos generado aparte)
├── SiGeP.Model/
├── SiGeP.DataAccess/
├── SiGeP.Business/
├── SiGeP.API/
└── SiGeP.UI/
```

### SiGeP.Model — dominio, DTOs, contexto EF

| Qué es | Dónde va |
|---|---|
| Entidad nueva | `SiGeP.Model/Model/<Entidad>.cs` (o subcarpeta temática, ej. `Model/Address/`, `Model/Person/`) heredando de `BaseEntity` |
| DTO de una entidad | `SiGeP.Model/DTO/<Entidad>DTO.cs` heredando de `BaseEntityDTO<int>` (o de otro DTO si comparte campos, ej. `CustomerDTO : PersonDTO`) |
| DTO genérico/transversal | `SiGeP.Model/BaseDTO/` (ej. `ActionResultDTO`, `ExceptionDTO`) |
| Clases base de entidad | `SiGeP.Model/Base/` (`BaseEntity`, `IEntity`, `ISoftDelete`, `PagedDataResponse`, `PagingSortFilterRequest`) |
| Migraciones EF | `SiGeP.Model/Migrations/` (generadas con `dotnet ef migrations add --project SiGeP.Model --startup-project SiGeP.API --context DbModelContext`) |
| Extensiones de EF/LINQ | `SiGeP.Model/Extensions/` (`ExpressionBuilder`, `EntityFrameworkExtension`) |
| Nuevo `DbSet` | Agregar a `DbSetEntities.cs` **y** registrar la entidad en `DbModelContext.OnModelCreating` con `modelBuilder.Entity<T>().HasKey(...)`. Ver nota en sección 4.2 sobre por qué `DbSetEntities` no es estrictamente necesario para que EF funcione, pero se sigue completando por consistencia. |
| Seed de datos iniciales | Método `SeedX` dentro de `ModelBuilderExtensions` en `DbModelContext.cs`, invocado desde `Seed()` |

### SiGeP.DataAccess — acceso a datos

| Qué es | Dónde va |
|---|---|
| Repositorio de una entidad | `SiGeP.DataAccess/Repositories/<Entidad>Repository.cs`, heredando de `GenericRepository<TEntidad>` |
| Registro del repositorio | Agregar la entrada `{ typeof(TEntidad), new <Entidad>Repository(_context) }` en el diccionario de `AddRepositories.cs` (ver sección 4.3 — este es el mecanismo real, **no** la inyección de dependencias vía `ServiceExtension`) |
| Lógica genérica CRUD/paginación/SQL crudo | `SiGeP.DataAccess/Generic/GenericRepository.cs` |
| Coordinación de transacciones/auditoría | `SiGeP.DataAccess/Generic/UnitOfWork.cs` |

### SiGeP.Business — reglas de negocio

| Qué es | Dónde va |
|---|---|
| Clase de negocio de una entidad | `SiGeP.Business/<Entidad>Business.cs`, heredando de `BusinessBase<TEntidad>` |
| Método de negocio específico (búsqueda, validación, regla) | Agregado directamente en la clase `XBusiness` (no hay capa de "servicios" adicional) |
| Registro en DI | `services.AddScoped<XBusiness, XBusiness>()` en `SiGeP.API/ServiceExtension.cs`, método `AddBusinessServices` |
| Interfaces de negocio | `SiGeP.Business/Interfaces/IBusiness.cs` |
| Clase base | `SiGeP.Business/Base/BusinessBase.cs` |

### SiGeP.API — endpoints REST

| Qué es | Dónde va |
|---|---|
| Controller nuevo | `SiGeP.API/Controllers/<Entidad>Controller.cs` |
| Registro del repositorio en DI (opcional/heredado) | `SiGeP.API/ServiceExtension.cs` → `AddDataAccessServices` |
| Registro del negocio en DI | `SiGeP.API/ServiceExtension.cs` → `AddBusinessServices` |
| Mapeo Entidad↔DTO | `SiGeP.API/Mapper/AutoMapperProfile.cs` |
| Modelos de auth/JWT | `SiGeP.API/Common.Model/` (`JwtTokenConfig`, `JwtAuthResult`, `RefreshToken`, `SigninRequest`, `WebApiConfig`, `UserContext`) |
| Infraestructura HTTP/cliente/config | `SiGeP.API/Common/` (`WebApiClient`, `WebApiClientExt2`, `AppConfiguration`, `BaseController`, `IJwtAuthManager`, `BusinessException`) |
| Logging | `SiGeP.API/LogConfiguration/SerilogConfiguration.cs` |
| Configuración de arranque | `Program.cs` (pipeline) y `ServiceExtension.cs` (registro de servicios) |

### SiGeP.UI — Blazor Server

| Qué es | Dónde va |
|---|---|
| Página nueva | `SiGeP.UI/Pages/<Nombre>.razor` con `@page "/ruta"` |
| Página de autenticación (Identity, Razor Pages clásicas) | `SiGeP.UI/Areas/Identity/Pages/Account/` |
| Componente reutilizable | `SiGeP.UI/Components/` |
| Layout compartido | `SiGeP.UI/Shared/` (`MainLayout.razor`, `NavMenu.razor`) |
| Servicio que llama a la API | `SiGeP.UI/Services/`, recibiendo `WebApiClient` (de `SiGeP.API.Common`) por constructor; registrarlo en `SiGeP.UI/ServiceExtension.cs` → `AddInfrastructureServices` |
| Validador de formulario | `SiGeP.UI/Helpers/`, heredando `AbstractValidator<T>` de FluentValidation |
| Modelos de datos propios de la UI (enums, view models) | `SiGeP.UI/Data/` |
| Recursos de localización Syncfusion | `SiGeP.UI/Resources/` |
| Estilos/CSS/imágenes | `SiGeP.UI/wwwroot/` |

## 4. Arquitectura, paradigmas y patrones de diseño

### 4.1 Estilo arquitectónico

Arquitectura **en capas** (n-tier), no hexagonal ni Clean Architecture estricta (no hay puertos/adaptadores ni
inversión de la dependencia hacia el dominio — `Business` depende directamente de `DataAccess.Generic.UnitOfWork`,
una clase concreta, no de una interfaz). El flujo de dependencias de proyecto es:

```
SiGeP.Model  ←  SiGeP.DataAccess  ←  SiGeP.Business  ←  SiGeP.API  ←  SiGeP.UI
                                                              ↑
                                                    SiGeP.UI también referencia
                                                    SiGeP.API directamente (ver 4.4)
```

El paradigma dominante es **OOP imperativo**, con uso puntual de LINQ/expresiones (`System.Linq.Expressions`) para
construir predicados dinámicos de filtro/orden (`SiGeP.Model/Extensions/ExpressionBuilder.cs`,
`EntityFrameworkExtension.cs`). No hay estilo funcional ni CQRS/MediatR.

### 4.2 Modelo de datos (`SiGeP.Model`)

Todas las entidades heredan de `BaseEntity` (`SiGeP.Model/Base/Base.cs`), que aporta `Id`, auditoría
(`Created`/`CreatedBy`, `Updated`/`UpdatedBy`, `Deleted`/`DeletedBy`) e implementa `IEntity` e `ISoftDelete`.

El borrado lógico se aplica automáticamente vía un **query filter global** de EF Core: en
`DbModelContext.AddMyFilters()` se recorre `modelBuilder.Model.GetEntityTypes()` y a cada entidad que implemente
`ISoftDelete` se le agrega el filtro `x => !x.Deleted.HasValue` (`DbContextExtension.AddSoftDeleteQueryFilter`).
`UnitOfWork.CompleteAsync()` intercepta el `ChangeTracker` antes de guardar: si una entidad está en estado
`Deleted` y es `ISoftDelete`, la reescribe como `Modified` seteando `Deleted`/`DeletedBy` en vez de borrarla
físicamente.

El `DbContext` (`DbModelContext.cs`) usa **lazy loading vía proxies** (`UseLazyLoadingProxies()`), motivo por el
cual toda navegación (`Person`, `Doctor`, `Address`, etc.) está declarada `virtual`. También tiene
`EnableSensitiveDataLogging()` activo — **TODO: revisar**, esto loguea valores de parámetros SQL y no debería
usarse en producción.

Curiosidad de diseño: existe una clase `DbSetEntities` con un `DbSet<T>` por entidad, expuesta como propiedad
`DbModelContext.DbSets`, pero **el contexto no tiene los `DbSet<T>` declarados directamente como propiedades**.
EF Core igual reconoce todas las entidades porque están registradas explícitamente en `OnModelCreating` vía
`modelBuilder.Entity<T>()`. En la práctica, el acceso a datos real pasa por `context.Set<TEntity>()` dentro de
`GenericRepository`, no por `DbSets`. Al agregar una entidad nueva hay que darla de alta en ambos lugares
igual, por consistencia con el patrón existente.

### 4.3 Repository + Service Locator (`SiGeP.DataAccess`)

`GenericRepository<TEntity>` (`SiGeP.DataAccess/Generic/GenericRepository.cs`) implementa CRUD, `GetPagedResultAsync`
(paginación + filtro + orden dinámico), y ejecución de SQL/SP crudo (`GetListFromRawSql`, `ExecuteSqlCommandAsync`).
Los repositorios específicos (`CustomerRepository`, `DoctorRepository`, etc.) son, en el estado actual, clases
vacías que solo heredan de `GenericRepository<T>` sin agregar miembros — toda la lógica de consulta específica que
existe hoy (`GetAllCitiesbyProvince`, `GetAllNeighborhoodsbyCity`, búsquedas por nombre) vive en la capa
**Business**, no en el repositorio, llamando a métodos genéricos (`GetAsync(filtro)`, `GetListAsync(filtro)`).

El acceso a un repositorio **no se hace por inyección de dependencias directa**, sino a través de un
**Service Locator**: `AddRepositories` (`SiGeP.DataAccess/Repositories/AddRepositories.cs`) mantiene un
`Dictionary<Type, object>` con una instancia de cada repositorio, y expone
`GetRepository<TEntity>()`. `UnitOfWork.AddRepositories` es una propiedad que instancia `AddRepositories`
perezosamente (`??=`). Todo el código de negocio accede así:
`unitOfWork.AddRepositories.GetRepository<Customer>().GetAsync(...)`.

**TODO: revisar** — `SiGeP.API/ServiceExtension.AddDataAccessServices()` sigue registrando cada repositorio
individualmente en el contenedor de DI (`services.AddScoped<CustomerRepository, CustomerRepository>()`, etc.), pero
nada los resuelve desde ahí: `UnitOfWork` solo recibe `DbModelContext` e `IHttpContextAccessor` por constructor, y
arma sus propios repositorios "a mano" dentro de `AddRepositories`. Esas líneas de `AddDataAccessServices` parecen
código muerto/remanente de un diseño anterior basado en DI pura.

`UnitOfWork` (`SiGeP.DataAccess/Generic/UnitOfWork.cs`) centraliza `CompleteAsync()`: recorre el `ChangeTracker`,
completa auditoría (usuario activo resuelto desde `IHttpContextAccessor.HttpContext.User.Identity.Name`) y aplica
el borrado lógico descripto en 4.2.

### 4.4 Capa de negocio (`SiGeP.Business`)

`BusinessBase<TEntity>` (`SiGeP.Business/Base/BusinessBase.cs`) implementa `IBusiness<TEntity>` con las operaciones
CRUD estándar (`FindAsync`, `GetAsync`, `GetPagedResultAsync`, `SaveAsync`, `DeleteAsync`), delegando siempre en
`unitOfWork.AddRepositories.GetRepository<TEntity>()`.

Varias clases de negocio (`CustomerBusiness`, `DoctorBusiness`, `AppointmentBusiness`) **sobrescriben `SaveAsync`**
para agregar control de concurrencia con `SemaphoreSlim` estático de instancia (`WaitAsync()`/`Release()` en
`try/finally`) — pensado para serializar altas/actualizaciones concurrentes de la misma entidad dentro del mismo
proceso. `AppointmentBusiness.SaveAsync` además valida reglas de negocio reales: que el turno no dure más de 3
horas y que no se superponga con otro turno existente (`SiGeP.Business/AppointmentBusiness.cs:29-49`).

**Nota sobre el patrón Observer**: una versión anterior de este documento mencionaba una infraestructura Observer
(`IObserver`, `ISubject`, `Notifier`, `PaymentObserver`, `ReminderObserver`) para notificar turnos/pagos. **Esa
infraestructura ya no existe en el código actual** (no están los archivos `SiGeP.Business/Interfaces/IObserver.cs`,
`ISubject.cs` ni la carpeta `SiGeP.Business/Notifiers/`), y tampoco existe `ReminderBusiness` ni
`ReminderRepository` pese a que la entidad `Reminder` sigue en el modelo y tiene su propio `SeedReminder`. Si se
retoma esa funcionalidad, no asumir que el patrón Observer sigue disponible: hay que reconstruirlo.

### 4.5 Patrones identificados (con archivo concreto)

| Patrón | Dónde | Nota |
|---|---|---|
| Repository | `GenericRepository<T>` + repos específicos | Genérico real, especializaciones hoy vacías |
| Service Locator | `AddRepositories.GetRepository<T>()` | Reemplaza lo que en otros proyectos sería DI de repositorios |
| Unit of Work | `UnitOfWork` | Centraliza `SaveChanges` + auditoría + soft delete |
| Template Method / clase base genérica | `BusinessBase<TEntity>`, `BaseEntity`, `BaseEntityDTO<Tid>`, `BaseController` | CRUD y campos comunes definidos una sola vez |
| DTO | `SiGeP.Model/DTO`, `SiGeP.Model/BaseDTO` | Separación entidad EF / contrato de API |
| Dependency Injection (contenedor ASP.NET Core) | `ServiceExtension.cs` en API y UI | `AddScoped` para business/servicios; ver 4.3 para la excepción de los repos |
| Query filter global (variante de Specification/soft delete) | `DbModelContext.AddMyFilters` | Aplica el filtro de borrado lógico a toda entidad `ISoftDelete` |
| Extension methods para querying dinámico | `EntityFrameworkExtension`, `ExpressionBuilder` | Orden/filtro por nombre de propiedad en runtime, incluso con path de navegación (`"Person.Name"`) |
| Semáforo para exclusión mutua en escritura | `CustomerBusiness`, `DoctorBusiness`, `AppointmentBusiness` (`SemaphoreSlim`) | Por-instancia (`static readonly` en cada clase, no compartido entre entidades) |

## 5. Seguridad: autenticación y autorización

**Leer con atención: el estado real difiere de lo que un README "ideal" describiría. Documentado tal cual está.**

### 5.1 Autenticación

Hay **dos mecanismos combinados**:

1. **JWT emitido por la API** (`SiGeP.API/Common/IJwtAuthManager.cs`, clase `JwtAuthManager`). Se generan
   `AccessToken` (HMAC-SHA256, configurable vía `JwtTokenConfig` en `appsettings.json`: `Secret`, `Issuer`,
   `Audience`, `AccessTokenExpiration`, `RefreshTokenExpiration`) y `RefreshToken` (guardado en memoria de proceso,
   `ConcurrentDictionary<string, RefreshToken>` — se pierde si la API se reinicia, **TODO: revisar** para producción
   con más de una instancia/reinicios frecuentes).
2. **Cookie de sesión en la UI** (`SiGeP.UI/Program.cs`, `AddAuthentication(CookieAuthenticationDefaults...).AddCookie`).
   El flujo real es:
   - `LoginPage.razor` (Blazor, `SiGeP.UI/Pages/Identity/Login.razor`) llama a
     `AuthenticationService.Authenticate` → `POST Authentication/Authenticate` en la API.
   - `AuthenticationBusiness.Authenticate` (`SiGeP.Business/AuthenticationBusiness.cs`) valida contra la tabla
     `AppUser` comparando **contraseña en texto plano** (`x.Password == password`, sin hashing).
     **TODO: revisar — es una vulnerabilidad real, no hay hashing de contraseñas para `AppUser`.**
   - La API devuelve `JwtAuthResult` (access + refresh token).
   - `LoginPage.razor` serializa ese resultado a JSON y navega a
     `Identity/Account/Login?jsonStr=...` (**el JWT completo viaja como query string**,
     `SiGeP.UI/Areas/Identity/Pages/Account/Login.cshtml.cs:39-58`). **TODO: revisar** — un token en la URL
     queda en logs de servidor/proxy e historial del navegador.
   - `LoginModel.OnGetAsync` deserializa el JWT, extrae sus claims (`ServiceExtensions.ParseClaimsFromJwt`) y hace
     `HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ...)`, es decir: **la cookie de la
     UI encapsula los claims del JWT** (incluye el `authToken` como claim, usado luego por `WebApiClient` para
     autorizar contra la API).
   - `WebApiClientExt2.ValidateAccessToken` (llamado desde los `Service` de la UI antes de cada request) chequea la
     expiración del claim `exp`, y si está por vencer llama a `Authentication/Refresh` para renovar el token sin
     desloguear al usuario.

- ASP.NET Core Identity está **scaffoldeado** (`SiGeP.UI/Areas/Identity/Pages/Account/*`: Login, Logout, Register,
  ForgotPassword, ResetPassword, ConfirmEmail) pero **no se usa como store de usuarios de negocio** — el login real
  de la aplicación pasa por `AppUser`/`AuthenticationBusiness`, no por `UserManager<TUser>` de Identity. Las páginas
  de Identity actúan como shell para levantar la cookie de sesión, no como sistema de cuentas independiente.

### 5.2 Autorización por endpoint

- `SiGeP.API/Common/BaseController.cs` tiene `[Authorize]` a nivel de clase.
- **`AuthenticationController`** es el único controller que hereda de `BaseController`, y está marcado
  `[AllowAnonymous]` (correcto, es el login).
- **Todos los demás controllers** (`CustomerController`, `DoctorController`, `AddressController`,
  `GenderController`, `AppointmentController`) heredan de `Controller` (no de `BaseController`) y tienen el
  atributo `[Authorize]` **comentado** en el código:
  ```csharp
  // SiGeP.API/Controllers/CustomerController.cs
  //[Authorize]
  [Route("[controller]")]
  [ApiController]
  public class CustomerController : Controller
  ```
  **TODO: revisar — esto es real y verificado en los 5 controllers de negocio: hoy, en el código actual, ningún
  endpoint de datos (pacientes, médicos, domicilios, géneros, turnos) exige token para responder.** El pipeline en
  `Program.cs` sí tiene `app.UseAuthentication()` / `app.UseAuthorization()`, y el middleware JWT está configurado
  correctamente — pero al no tener `[Authorize]` activo en los controllers, la protección no se aplica. Antes de
  exponer este backend fuera de un entorno de desarrollo hay que descomentar `[Authorize]` (o mejor, hacer heredar
  estos controllers de `BaseController`) en los 5 controllers listados arriba.

  Ejemplo de cómo debería quedar un endpoint protegido (siguiendo el único caso ya correcto, `AuthenticationController`,
  como modelo de herencia — no de `[AllowAnonymous]`):
  ```csharp
  [Route("[controller]")]
  [ApiController]
  public class CustomerController : BaseController   // hereda BaseController, no Controller
  {
      public CustomerController(IMapper mapper, IHttpContextAccessor contextAccessor,
          CustomerBusiness customerBusiness, IJwtAuthManager jwtAuthManager) : base(jwtAuthManager)
      { ... }
  }
  ```

- No hay autorización basada en roles (`[Authorize(Roles = "...")]`) implementada en ningún controller, aunque el
  modelo tiene `Role` con un campo `Permissions` de texto libre (`SiGeP.Model/ModelUser/Role.cs`) que no se usa
  todavía para autorizar nada.

### 5.3 Dónde está centralizada la configuración de seguridad

- JWT: sección `JwtTokenConfig` en `SiGeP.API/appsettings.json`, cableado en `SiGeP.API/Program.cs:89-114`.
- Cookie: `SiGeP.UI/Program.cs:15-27` (`LoginPath = "/LoginPage"`, `SlidingExpiration = true`).
- No existe middleware propio de manejo de excepciones actualmente. **TODO: revisar** — versiones anteriores del
  proyecto tenían `SiGeP.API/LogConfiguration/ExceptionHandlingMiddleware.cs`; ese archivo ya no está en el repo y
  no se lo referencia en `Program.cs`. Hoy el manejo de errores es inconsistente entre controllers: `CustomerController`
  envuelve cada acción en `try/catch` y devuelve `StatusCode(500, new ActionResultDTO {...})`; `DoctorController`,
  `AddressController`, `GenderController` y `AppointmentController` **no tienen try/catch**, por lo que una excepción
  ahí se propaga sin formatear (comportamiento por defecto de ASP.NET Core, página de excepción de desarrollador
  solo en `Development`).
- Logging con Serilog: configurado en `SiGeP.API/appsettings.json` (sección `Serilog`, sink `MSSqlServer` hacia
  `DatabaseLogsSiGeP`) y `SerilogConfiguration.cs`. **TODO: revisar** — en `Program.cs:26-38` el bloque que arma
  `Log.Logger` con `LoggerConfiguration()...CreateLogger()` está **comentado**, y solo queda `builder.Host.UseSerilog()`
  sin logger explícito ni `ReadFrom.Configuration`. Verificar si los logs realmente están llegando a
  `DatabaseLogsSiGeP` en el estado actual antes de asumir que el logging funciona end-to-end.
- **Credenciales en texto plano en `appsettings.json`**: `ConnectionStrings:DefaultConnection` (API) y la sección
  `Serilog:WriteTo:Args:connectionString` incluyen usuario/contraseña de SQL Server en texto plano, y el archivo
  **está trackeado en git** (`git ls-files` confirma `SiGeP/SiGeP.API/appsettings.json` como versionado) a pesar de
  figurar en `.gitignore` — el `.gitignore` se agregó después de que el archivo ya estuviera commiteado, así que no
  tiene efecto retroactivo. `JwtTokenConfig:Secret` también está en texto plano en el mismo archivo. **TODO: revisar
  con prioridad**: sacar el archivo del tracking de git (`git rm --cached`) y rotar credenciales/secreto, idealmente
  moviendo estos valores a `dotnet user-secrets` (ya hay `UserSecretsId` configurado en ambos `.csproj`) o variables
  de entorno.

## 6. Convenciones de código y estilo

Relevado de archivos reales, no es una guía de estilo formal declarada en el repo (no hay `.editorconfig` con
reglas de nombrado ni analizador configurado más allá del default de Visual Studio).

- **Namespaces**: reflejan la ruta de carpetas (`SiGeP.Model.Model.Address`, `SiGeP.API.Common.Model`, etc.).
- **Clases de negocio/repositorio**: `<Entidad>Business`, `<Entidad>Repository` (PascalCase, sufijo por rol).
- **Campos privados**: mezcla real de dos convenciones en el mismo repo — `private readonly Foo foo;` (sin guión
  bajo, ej. `CustomerController`) y `private readonly Foo _foo;` (con guión bajo, ej. `DoctorController`,
  `AddressController`). **No hay una convención única**; al tocar un archivo existente, seguir el estilo que ya
  tiene ese archivo en particular en vez de imponer uno nuevo.
- **Async**: todos los métodos de acceso a datos y negocio son `async Task<T>` / `async Task`, con sufijo `Async`.
  Hay algunos wrappers síncronos que bloquean sobre una tarea (`GenericRepository.Get` usa
  `Task.Run(async () => await GetAsync(...)).Result`) — evitar replicar ese patrón en código nuevo, es una fuente
  potencial de deadlocks; usar directamente la versión `Async`.
- **Manejo de errores**: no es uniforme (ver 5.3). Donde existe, sigue el patrón
  `try { ... } catch (Exception ex) { return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" }); }`
  en los controllers, y `try/finally` con semáforo en las clases de negocio que lo usan (sección 4.4). Se ve además
  el antipatrón `catch (Exception ex) { throw ex; }` (pierde el stack trace original) en `CustomerBusiness.SaveAsync`,
  `DoctorBusiness.SaveAsync`, `AppointmentBusiness.SaveAsync` — si se toca ese código, cambiar a `throw;` a secas.
- **Comentarios**: escasos y mayormente en español, suelen marcar código pendiente o dudas del autor
  (`//TODO: solo para pureba`, `// Prueba con semáforo`, `//ACA DEFINIR LOS REPOSITORIOS <---`). Sirven como señal
  real de partes en desarrollo activo — no ignorarlos.
- **Idioma**: identificadores de código en inglés (`CustomerBusiness`, `GetAsync`), mensajes de usuario/negocio y
  comentarios en español (`"El cliente se actualizó correctamente"`).
- **Regiones (`#region`)**: usadas con frecuencia para separar bloques dentro de una clase (Vars, Methods, Actions,
  GET/ADD/UPDATE/DELETE). Mantener esa convención al extender una clase existente que ya las usa.
- **DataAnnotations para columnas**: `[Column(TypeName = "VARCHAR"), StringLength(n)]` es el patrón estándar para
  strings en entidades (ver `Person`, `AppUser`, `Role`, `Doctor`). Usarlo en toda propiedad `string` nueva de una
  entidad.

## 7. Plantillas de clases por tipo

Basadas en los ejemplos reales ya citados. Reemplazar `Foo`/`foo` por el nombre de la entidad nueva.

### Entidad (`SiGeP.Model/Model/Foo.cs`)

```csharp
using SiGeP.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiGeP.Model.Model
{
    public class Foo : BaseEntity
    {
        [Column(TypeName = "VARCHAR"), StringLength(128)]
        public string Name { get; set; }

        // FK + navegación, si aplica (virtual por lazy loading)
        public int OtherId { get; set; }
        public virtual Other Other { get; set; }
    }
}
```
Luego: agregar `DbSet<Foo> Foo` en `DbSetEntities.cs` y `modelBuilder.Entity<Foo>().HasKey(x => x.Id);` en
`DbModelContext.OnModelCreating` (sección 4.2).

### DTO (`SiGeP.Model/DTO/FooDTO.cs`)

```csharp
using SiGeP.Model.BaseDTO;

namespace SiGeP.Model.DTO
{
    public class FooDTO : BaseEntityDTO<int>
    {
        public string Name { get; set; }
    }
}
```

### Repositorio (`SiGeP.DataAccess/Repositories/FooRepository.cs`)

```csharp
using SiGeP.DataAccess.Generic;
using SiGeP.Model;
using SiGeP.Model.Model;

namespace SiGeP.DataAccess.Repositories
{
    public class FooRepository : GenericRepository<Foo>
    {
        public FooRepository(DbModelContext context) : base(context)
        {
        }
    }
}
```
Y agregarlo en `AddRepositories.cs`: `{ typeof(Foo), new FooRepository(_context) }`.

### Clase de negocio (`SiGeP.Business/FooBusiness.cs`)

```csharp
using SiGeP.Business.Base;
using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model;

namespace SiGeP.Business
{
    public class FooBusiness : BusinessBase<Foo>
    {
        public FooBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // Métodos específicos, ej.:
        public async Task<IEnumerable<Foo>> GetListAsync(string name)
        {
            return await unitOfWork.AddRepositories.GetRepository<Foo>()
                .GetListAsync(x => x.Name.ToUpper().Contains(name.ToUpper()));
        }
    }
}
```
Registrar en `SiGeP.API/ServiceExtension.AddBusinessServices`: `services.AddScoped<FooBusiness, FooBusiness>();`

### Controller (`SiGeP.API/Controllers/FooController.cs`)

```csharp
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiGeP.Business;
using SiGeP.Model.Base;
using SiGeP.Model.BaseDTO;
using SiGeP.Model.DTO;
using SiGeP.Model.Model;

namespace SiGeP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FooController : BaseController   // no "Controller" a secas — ver sección 5.2
    {
        private readonly IMapper mapper;
        private readonly FooBusiness fooBusiness;

        public FooController(IMapper mapper, FooBusiness fooBusiness, IJwtAuthManager jwtAuthManager)
            : base(jwtAuthManager)
        {
            this.mapper = mapper;
            this.fooBusiness = fooBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FooDTO>>> GetAll()
        {
            var list = await fooBusiness.GetAsync();
            return Ok(mapper.Map<IList<FooDTO>>(list));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FooDTO>> GetById(int id)
        {
            var item = await fooBusiness.FindAsync(id);
            if (item == null) return NotFound(new ActionResultDTO { Message = $"No se encontró Foo con ID {id}" });
            return Ok(mapper.Map<FooDTO>(item));
        }

        [HttpGet("PagedData")]
        public async Task<ActionResult<PagedDataResponse<FooDTO>>> GetPaged([FromQuery] PagingSortFilterRequest request)
        {
            var result = await fooBusiness.GetPagedResultAsync(request);
            return Ok(new PagedDataResponse<FooDTO>
            {
                PageCount = result.PageCount,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                RowCount = result.RowCount,
                Results = mapper.Map<List<FooDTO>>(result.Results)
            });
        }

        [HttpPost]
        public async Task<ActionResult<ActionResultDTO>> Add([FromBody] FooDTO dto)
        {
            var entity = mapper.Map<Foo>(dto);
            var result = await fooBusiness.SaveAsync(entity);
            return Ok(new ActionResultDTO
            {
                Code = result.ToString(),
                Message = dto.Id > 0 ? "Se actualizó correctamente" : "Se registró correctamente"
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ActionResultDTO>> Delete(int id)
        {
            var result = await fooBusiness.DeleteAsync(id);
            return Ok(new ActionResultDTO { Code = result.ToString(), Message = "Se eliminó correctamente" });
        }
    }
}
```
Agregar el mapeo `CreateMap<Foo, FooDTO>().ReverseMap();` en `AutoMapperProfile.cs`.

### Componente Blazor (`SiGeP.UI/Components/FooComponent.razor`)

Ver sección 8.2 — hoy no hay un componente CRUD genérico y parametrizable para copiar como base; el que existe
(`ABMComponent.razor`) es un formulario fijo, no reutilizable tal cual. Para un componente de UI nuevo, seguir el
esqueleto de `MessageCardComponent.razor` (parámetros vía `[Parameter]`, `EventCallback<T>` para comunicar eventos
al padre, `StateHasChanged()` tras cambios de estado):

```razor
@using SiGeP.Model.DTO

<!-- markup -->

@code {
    [Parameter]
    public EventCallback<bool> AlgoChanged { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // carga inicial
    }

    private async Task NotifyAlgo(bool value)
    {
        await AlgoChanged.InvokeAsync(value);
        StateHasChanged();
    }
}
```

## 8. Frontend: pantallas, componentes, estado

### 8.1 Pantallas actuales

| Ruta | Archivo | Estado / propósito |
|---|---|---|
| `/` | `Pages/Index.razor` | Home |
| `/LoginPage` | `Pages/Identity/Login.razor` | Login custom (Blazor), llama a la API y redirige a la página de Identity para setear la cookie (sección 5.1) |
| `/CustomerCRUD` | `Pages/CustomerCRUD.razor` | **Incompleta**: el archivo solo tiene bloque `@code` (inyecta `ProvinceService`, carga provincias en `OnInitialized`), sin ningún markup/UI todavía. Es el destino del link "Lista de Pacientes" del menú. |
| `/ABMComponent` | `Components/ABMComponent.razor` | Ver 8.2 |
| `Identity/Account/Login` | `Areas/Identity/Pages/Account/Login.cshtml` | Razor Page (no Blazor) que recibe el JWT y crea la cookie de sesión |
| `Identity/Account/Logout`, `Register`, `ForgotPassword`, `ResetPassword`, `ConfirmEmail`, `*Confirmation` | `Areas/Identity/Pages/Account/*` | Scaffolding estándar de ASP.NET Core Identity, sin personalización de negocio relevada |
| `/Error` | `Pages/Error.cshtml` | Página de error genérica de ASP.NET Core |

### 8.2 Componentes

| Componente | Archivo | Propósito real |
|---|---|---|
| `ABMComponent` | `Components/ABMComponent.razor` | **No es genérico ni parametrizable**: es un formulario fijo con campos de Persona (Nombre, Apellido, DNI, Provincia, Teléfono, Mail) y un botón "Guardar" que **no tiene handler conectado** (`SfButton` sin `OnClick`). Ruteable en `/ABMComponent`, no se usa embebido en ninguna otra página actualmente. **Cualquier documentación previa que lo describa como "componente base reutilizable para todo CRUD" está desactualizada — no reflejaba el código real.** |
| `LoginRedirect` | `Components/LoginRedirect.razor` | Se muestra cuando `AuthorizeRouteView` determina que el usuario no está autorizado (ver `App.razor`); redirige a `/LoginPage` y arma un timer de inactividad (la desconexión automática por JS está comentada, `// await JSRuntime.InvokeVoidAsync("timeOutCall", ...)`, **TODO: revisar**, no está activa) |
| `MessageCardComponent` | `Components/MessageCardComponent.razor` | Modal de mensajes genérico (Information/Warning/Error/Question/Confirmation), instanciado una vez en `MainLayout` y expuesto a las páginas vía `mainLayoutObj.ShowCardMessage(...)`. Es el componente que mejor sigue un patrón reutilizable real hoy. |

`ActionButtonComponent.razor` y `PatientsListComponent.razor`, presentes en versiones anteriores del proyecto,
**ya no existen en el código actual** — no asumir que siguen disponibles.

### 8.3 Layout y navegación

- `App.razor`: define el `Router` con `CascadingAuthenticationState` + `AuthorizeRouteView`; si no está autorizado
  renderiza `LoginRedirect`.
- `MainLayout.razor`: layout raíz. Envuelve todo en `CascadingValue Value="@this"` para que las páginas hijas
  puedan llamar `mainLayoutObj.ShowCardMessage(...)` / `ShowSpinner()` / `HideSpinner()` vía `[CascadingParameter]`.
  Muestra `NavMenu` y el botón de logout solo si `AuthorizeView` indica usuario autenticado.
- `NavMenu.razor`: menú lateral colapsable; hoy solo tiene dos links, "Inicio" y "Lista de Pacientes" (→
  `/CustomerCRUD`, que como se indicó en 8.1 todavía no tiene UI).
- `_Imports.razor` tiene `@attribute [Authorize]` a nivel global — **todas las páginas Blazor requieren
  autenticación por defecto**, salvo las marcadas explícitamente `@attribute [AllowAnonymous]` (`LoginPage.razor`,
  `LoginRedirect.razor`). Esta es la única capa de autorización real y consistente que existe hoy en todo el
  sistema (a diferencia de la API, ver sección 5.2).

### 8.4 Manejo de estado

No hay una librería de manejo de estado (no Fluxor, no Redux-like). El estado se maneja con:
- **Cascading values/parameters** para comunicar layout ↔ página (`MainLayout` ↔ páginas hijas).
- **Servicios `Scoped`** inyectados (`AuthenticationService`, `ProvinceService`) que envuelven al `WebApiClient` y
  no mantienen estado propio entre llamadas — cada método hace una llamada HTTP nueva.
- **Claims de la cookie de autenticación** como almacenamiento del `authToken`/`refreshToken` (ver sección 5.1) —
  es, en la práctica, el "estado de sesión" de la aplicación.
- Variables locales `@code` por componente/página (ej. `provinceList`, `enabled` en `ABMComponent`), sin patrón de
  estado compartido entre componentes salvo lo ya mencionado.

### 8.5 Convención de nombres/ubicación de archivos de UI

- Página con ruta propia → `Pages/<Nombre>.razor` con `@page "/ruta"` en la primera línea.
- Página de un módulo (ej. Identity) → subcarpeta bajo `Areas/<Area>/Pages/...`, siguiendo la convención de
  Razor Pages/Areas de ASP.NET Core, no de Blazor puro.
- Componente sin ruta propia (para embeber) → `Components/<Nombre>Component.razor` (sufijo `Component` es la
  convención usada, ver `MessageCardComponent`, no siempre respetada — `LoginRedirect` no lo lleva).
- CSS específico de un componente/layout → archivo scoped `<Nombre>.razor.css` junto al `.razor` (ver
  `MainLayout.razor.css`, `NavMenu.razor.css`).

## 9. Guía de uso y checklist rápido

**Cómo leer este documento antes de escribir un prompt o encarar una tarea:**
1. Si la tarea toca **datos** (nueva entidad, campo, relación): leer sección 3 (Model/DataAccess) y 4.2.
2. Si toca **reglas de negocio**: leer 4.3 y 4.4, y copiar el patrón de `BusinessBase`/semáforo si hay
   concurrencia involucrada.
3. Si toca **un endpoint**: leer sección 5.2 antes de asumir que está protegido — hoy, por defecto, no lo está.
4. Si toca **la UI**: leer sección 8 completa antes de asumir que existe un componente CRUD reutilizable — no
   existe todavía en forma utilizable.
5. Si la tarea es de **seguridad o vas a tocar `appsettings.json`**: leer 5.3 primero, no commitear secretos
   nuevos en texto plano (usar `dotnet user-secrets set` dado que `UserSecretsId` ya está configurado).

**Checklist para agregar una entidad de negocio nueva end-to-end:**

- [ ] Entidad en `SiGeP.Model/Model/` heredando `BaseEntity` (sección 7)
- [ ] `DbSet` agregado en `DbSetEntities.cs`
- [ ] Entidad registrada en `DbModelContext.OnModelCreating` (`HasKey`, y `Seed` si corresponde)
- [ ] Migración generada: `dotnet ef migrations add <Nombre> --project SiGeP.Model --startup-project SiGeP.API --context DbModelContext`
- [ ] DTO en `SiGeP.Model/DTO/`
- [ ] Mapeo agregado en `SiGeP.API/Mapper/AutoMapperProfile.cs`
- [ ] Repositorio en `SiGeP.DataAccess/Repositories/`, dado de alta en `AddRepositories.cs`
- [ ] Clase de negocio en `SiGeP.Business/`, registrada en `ServiceExtension.AddBusinessServices` (API)
- [ ] Controller en `SiGeP.API/Controllers/`, heredando `BaseController` (no `Controller` a secas) con
      `[Authorize]` activo salvo que deba ser público
- [ ] Página/componente Blazor en `SiGeP.UI/Pages` o `Components`, consumiendo un `Service` propio en
      `SiGeP.UI/Services` registrado en `ServiceExtension.AddInfrastructureServices` (UI)
- [ ] Si hay dato sensible nuevo en `appsettings.json`, evaluar `user-secrets` en vez de texto plano

**Deuda técnica conocida (no bloqueante para seguir desarrollando, pero a tener presente):**
- Autorización deshabilitada en los 5 controllers de negocio (sección 5.2).
- Contraseñas de `AppUser` sin hash (sección 5.1).
- JWT viajando por query string en el flujo de login (sección 5.1).
- Credenciales de SQL Server y secreto JWT en texto plano, ya commiteados en git (sección 5.3).
- Sin middleware centralizado de manejo de excepciones; comportamiento inconsistente entre controllers (sección 5.3).
- Serilog probablemente no está escribiendo a `DatabaseLogsSiGeP` (inicialización comentada en `Program.cs`, sección 5.3).
- Registro de repositorios en DI (`AddDataAccessServices`) parece código muerto (sección 4.3).
- `ABMComponent`/`CustomerCRUD` no son funcionales todavía como CRUD de pacientes (sección 8.1/8.2).
- Sin proyecto de tests automatizados en la solución.
- Versión de `Syncfusion.Blazor.Grid` (19.2.0.55) desalineada respecto del resto de paquetes Syncfusion (27.1.50).
