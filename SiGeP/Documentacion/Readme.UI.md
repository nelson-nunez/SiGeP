# Aplicación Blazor Server UI

Bitácora de configuración de la interfaz de usuario (Blazor Server), con Syncfusion, Identity y FluentValidation.

## 1. Dependencias e inicialización

- Instalar Syncfusion y Bootstrap (versión 19.0.2.55).
- Agregar referencias en `_Imports.razor` para Syncfusion y Bootstrap.
- Agregar la licencia de Syncfusion en `Startup.cs` para habilitar la funcionalidad completa.
- Agregar las clases comunes necesarias para la aplicación en una carpeta como `Common` o `Shared`.

## 2. Configuración de servicios y datos

- Configurar servicios y acceso a datos en `Startup.cs`, incluyendo la configuración de `ApplicationDbContext`.
- Configurar la cultura en `Startup.cs` para evitar problemas de conversión con horas y monedas.
- Agregar la extensión `AddInfrastructureServices` en `Startup.cs` para organizar mejor los servicios de Blazor.
- Agregar `ApplicationDbContext` en `Startup.cs` para poder escafoldar Identity.

## 3. Identity

- Scaffoldear Identity seleccionando las páginas básicas necesarias: login, logout, reset password, email confirmation.
- Agregar en `Host.cshtml` los enlaces de CDN o los paquetes descargados de CSS y JS necesarios.
- Agregar la directiva `@attribute [Authorize]` en `_Imports.razor` para requerir autorización en todas las páginas, excepto las marcadas con `[AllowAnonymous]`.
- Scaffoldear Identity nuevamente si es necesario, agregando referencias y creando el componente `LoginRedirect`. Modificar el modelo anterior según sea necesario.

## 4. Validaciones (FluentValidation)

- Agregar `FluentValidation` vía NuGet y crear una clase custom de validación en la carpeta `Helpers`. Luego, agregar las clases de validación para cada formulario en `Helpers`.
- Modificar las páginas de Identity para que respondan correctamente a las llamadas y validaciones recreadas en Blazor. Registrar en las mismas el servicio de autenticación propio (mediante `using`).
