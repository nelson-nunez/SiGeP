Aplicaci�n Blazor Server UI:

* Instalar Syncfusion y Bootstrap (versi�n 19.0.2.55).
* Agregar referencias en Imports para Syncfusion y Bootstrap.
* Agregar la licencia de Syncfusion en Startup.cs para habilitar la funcionalidad completa.
* Agregar clases comunes necesarias para la aplicaci�n en una carpeta como Common o Shared.
* Configurar servicios y acceso a datos en Startup.cs, incluyendo la configuraci�n de ApplicationDbContext.
* Configurar la cultura en Startup.cs para evitar problemas de conversi�n con horas y monedas.
* Agregar la extensi�n AddInfrastructureServices en Startup.cs para organizar mejor los servicios de Blazor.
* Agregar ApplicationDbContext en Startup.cs para poder escafoldar Identity.
* Scafoldear Identity seleccionando las p�ginas b�sicas necesarias, como login, logout, reset password, email confirmation.
* Agregar en Host.cshtml los enlaces de CDN o paquetes descargados de CSS y JS necesarios.
* Agregar la directiva @attribute [Authorize] en Imports.razor para requerir autorizaci�n en todas las p�ginas, excepto las marcadas con [AllowAnonymous].
* Scafoldear Identity nuevamente si es necesario, agregando referencias y creando el componente LoginRedirect. Modificar el modelo antiguo seg�n sea necesario.
* Agregar FluentValidation en NuGet y crear una clase custom de validaci�n en la carpeta Helpers. Luego, agregar las clases de validaci�n para cada formulario en Helpers.
* Modificar las p�ginas de Identity para que respondan correctamente a las llamadas y validaciones recreadas en Blazor. Registrar en las mismas el servicio de autenticacion nuestro con using