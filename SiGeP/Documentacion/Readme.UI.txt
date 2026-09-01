Aplicación Blazor Server UI:

* Instalar Syncfusion y Bootstrap (versión 19.0.2.55).
* Agregar referencias en Imports para Syncfusion y Bootstrap.
* Agregar la licencia de Syncfusion en Startup.cs para habilitar la funcionalidad completa.
* Agregar clases comunes necesarias para la aplicación en una carpeta como Common o Shared.
* Configurar servicios y acceso a datos en Startup.cs, incluyendo la configuración de ApplicationDbContext.
* Configurar la cultura en Startup.cs para evitar problemas de conversión con horas y monedas.
* Agregar la extensión AddInfrastructureServices en Startup.cs para organizar mejor los servicios de Blazor.
* Agregar ApplicationDbContext en Startup.cs para poder escafoldar Identity.
* Scafoldear Identity seleccionando las páginas básicas necesarias, como login, logout, reset password, email confirmation.
* Agregar en Host.cshtml los enlaces de CDN o paquetes descargados de CSS y JS necesarios.
* Agregar la directiva @attribute [Authorize] en Imports.razor para requerir autorización en todas las páginas, excepto las marcadas con [AllowAnonymous].
* Scafoldear Identity nuevamente si es necesario, agregando referencias y creando el componente LoginRedirect. Modificar el modelo antiguo según sea necesario.
* Agregar FluentValidation en NuGet y crear una clase custom de validación en la carpeta Helpers. Luego, agregar las clases de validación para cada formulario en Helpers.
* Modificar las páginas de Identity para que respondan correctamente a las llamadas y validaciones recreadas en Blazor. Registrar en las mismas el servicio de autenticacion nuestro con using