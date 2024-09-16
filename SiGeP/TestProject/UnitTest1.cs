using Moq;
using Microsoft.AspNetCore.Mvc;
using SiGeP.API.Controllers;
using SiGeP.Business;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using FluentAssertions;
using SiGeP.Model.BaseDTO;
using SiGeP.Model.DTO;
using SiGeP.Model.Model;

namespace SiGeP.UniTestProject
{
    public class CustomerControllerTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<CustomerBusiness> _mockCustomerBusiness;
        private CustomerController _controller;

        // Método que se ejecuta antes de cada test
        [SetUp]
        public void Setup()
        {
            // Crear mocks para las dependencias del controlador
            _mockMapper = new Mock<IMapper>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockCustomerBusiness = new Mock<CustomerBusiness>(null);

            // Inicializar el controlador con los mocks inyectados
            _controller = new CustomerController(
                _mockMapper.Object,
                _mockHttpContextAccessor.Object,
                _mockCustomerBusiness.Object);
        }

        // Test para verificar que el método GetAllResellers devuelva un estado 200 OK cuando hay datos
        [Test]
        public async Task GetAllResellers_ShouldReturnOk_WhenDataIsReturned()
        {
            // Arrange: configurar los datos simulados
            var customers = new List<Customer> { new Customer { Id = 1 } };
            var customerDTOs = new List<CustomerDTO> { new CustomerDTO { Id = 1 } };

            // Configuración de los mocks
            _mockCustomerBusiness.Setup(x => x.GetAsync()).ReturnsAsync(customers);
            _mockMapper.Setup(m => m.Map<IList<CustomerDTO>>(customers)).Returns(customerDTOs);

            // Act: invocar el método del controlador
            var result = await _controller.GetAllCustomers();

            // Assert: validar que el resultado sea correcto
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull(); // Verificar que se obtiene un resultado de tipo OK
            okResult.StatusCode.Should().Be(200); // Verificar que el estado es 200 OK
            okResult.Value.Should().BeEquivalentTo(customerDTOs); // Validar que los datos devueltos son correctos
        }
  
        // Test para verificar que el método Add devuelva 200 OK cuando un cliente es agregado exitosamente
        [Test]
        public async Task Add_ShouldReturnOk_WhenCustomerIsAdded()
        {
            // Arrange: datos de entrada simulados
            var customerDTO = new CustomerDTO { Id = 0 };
            var customer = new Customer { Id = 0 };

            // Configurar mocks
            _mockMapper.Setup(m => m.Map<Customer>(customerDTO)).Returns(customer);
            _mockCustomerBusiness.Setup(x => x.SaveAsync(customer)).ReturnsAsync(1);

            // Act: invocar el método del controlador
            var result = await _controller.Add(customerDTO);

            // Assert: validar que el resultado es correcto
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull(); // Verificar que el resultado es OK
            okResult.StatusCode.Should().Be(200); // Verificar que el estado es 200 OK
            var response = okResult.Value as ActionResultDTO;
            response.Should().NotBeNull(); // Validar que hay una respuesta correcta
            response.Message.Should().Be("El cliente se registró correctamente"); // Validar el mensaje devuelto
        }

        // Test para verificar que se devuelva un error cuando no se puede agregar un cliente
        [Test]
        public async Task Add_ShouldReturnBadRequest_WhenAddFails()
        {
            // Arrange: simular un error al agregar el cliente
            var customerDTO = new CustomerDTO { Id = 0 };
            var customer = new Customer { Id = 0 };

            _mockMapper.Setup(m => m.Map<Customer>(customerDTO)).Returns(customer);
            _mockCustomerBusiness.Setup(x => x.SaveAsync(customer)).ReturnsAsync(0); // Simular fallo en guardar

            // Act: invocar el método del controlador
            var result = await _controller.Add(customerDTO);

            // Assert: validar que el resultado es un BadRequest
            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            var response = badRequestResult.Value as ActionResultDTO;
            response.Should().NotBeNull();
            response.Message.Should().Be("Error al registrar el cliente"); // Verificar mensaje de error
        }


        [TearDown]
        public void TearDown()
        {
            // Liberar los mocks si implementan IDisposable
            _controller?.Dispose();
        }
    }
}
