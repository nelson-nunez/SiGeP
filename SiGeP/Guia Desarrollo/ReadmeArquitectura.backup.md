# SiGeP - Arquitectura del Sistema

SiGeP (Sistema de Gestión de Pacientes) es una aplicación .NET 8.0 organizada en cinco proyectos dentro de una misma solución:
SiGeP.Model, SiGeP.DataAccess, SiGeP.Business, SiGeP.API y SiGeP.UI. 
Esta separación implementa una arquitectura por capas donde cada proyecto tiene una responsabilidad exclusiva, 
y las dependencias fluyen siempre hacia abajo: UI → API → Business → DataAccess → Model.

El proyecto SiGeP.Model centraliza todo lo relacionado con datos: entidades de dominio, DTOs, 
DbContext de EF Core 8 y migraciones. Todas las entidades heredan de BaseEntity, que provee Id,
auditoría automática (Created, Updated, Deleted con usuario) y soporte de borrado lógico mediante 
la interfaz ISoftDelete. El DbContext tiene lazy loading habilitado vía proxies y filtros de consulta 
globales que excluyen automáticamente los registros eliminados de forma lógica.

SiGeP.DataAccess implementa el patrón Repository con una clase genérica GenericRepository que cubre CRUD,
paginación y ejecución de SQL raw. Sobre esto opera el Unit of Work, que coordina las transacciones, 
aplica los timestamps de auditoría y resuelve el usuario activo desde el IHttpContextAccessor antes de cada 
SaveChanges. Los repositorios específicos (CustomerRepository, DoctorRepository, etc.) extienden el genérico 
solo cuando necesitan queries particulares.

SiGeP.Business contiene las reglas de negocio a través de clases que heredan de BusinessBase. 
Aquí vive la validación de datos, el control de concurrencia con SemaphoreSlim para operaciones críticas 
y la infraestructura del patrón Observer para notificaciones de turnos y pagos. SiGeP.API expone los
endpoints REST, aplica AutoMapper para convertir entre entidades y DTOs, y protege todos los controladores 
con JWT Bearer. La autenticación usa access token (200 min) y refresh token (600 min) configurables desde appsettings.json.

SiGeP.UI es un frontend Blazor Server que consume la API mediante un WebApiClient genérico. Usa autenticación por cookies, componentes Syncfusion para grillas y formularios, FluentValidation para validación en cliente, y está configurado en cultura es-ES. El componente ABMComponent es la pieza central reutilizable: encapsula la lógica de listado, creación, edición y eliminación, y debe usarse como base para cualquier nueva pantalla CRUD del sistema.

Al agregar una nueva entidad al sistema se debe seguir este orden: crear la clase en SiGeP.Model heredando de BaseEntity y agregar el DbSet al contexto, generar el DTO correspondiente y el mapeo en AutoMapperProfile, crear el repositorio en SiGeP.DataAccess registrándolo en UnitOfWork, implementar la clase de negocio en SiGeP.Business, exponer el controlador en SiGeP.API con los atributos Authorize y los métodos GET/POST/DELETE usando paginación donde corresponda, y finalmente crear la página Razor en SiGeP.UI reutilizando ABMComponent. Este flujo garantiza consistencia con los patrones ya establecidos en el proyecto.
