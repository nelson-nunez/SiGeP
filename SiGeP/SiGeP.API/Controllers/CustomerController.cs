using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SiGeP.Business;
using SiGeP.Model.Base;
using SiGeP.Model.BaseDTO;
using SiGeP.Model.DTO;
using SiGeP.Model.Model;

namespace SiGeP.API.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;
        private readonly CustomerBusiness customerBusiness;

        public CustomerController(IMapper mapper, IHttpContextAccessor contextAccessor, CustomerBusiness customerBusiness)
        {
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.customerBusiness = customerBusiness;
        }

        [HttpGet()]
        public async Task<ActionResult<IList<CustomerDTO>>> GetAllCustomers()
        {
            try
            {
                var list = await customerBusiness.GetAsync();
                if (list == null || !list.Any())
                    return NotFound(new ActionResultDTO { Message = "No se encontraron clientes" });

                var dto = mapper.Map<IList<CustomerDTO>>(list);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerbyId(int id)
        {
            try
            {
                var item = await customerBusiness.FindAsync(id);
                if (item == null)
                    return NotFound(new ActionResultDTO { Message = $"No se encontró un cliente con el ID {id}" });

                var dto = mapper.Map<CustomerDTO>(item);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
            }
        }

        [HttpGet("PagedData")]
        public async Task<ActionResult<PagedDataResponse<CustomerDTO>>> GetAllCustomers([FromQuery] PagingSortFilterRequest request)
        {
            try
            {
                var orderDirection = "";
                if (!string.IsNullOrEmpty(request.OrderBy) && request.OrderBy[0] == '-')
                {
                    orderDirection = "-";
                    request.OrderBy = request.OrderBy.Substring(1);
                }
                switch (request.OrderBy)
                {
                    case "Name":
                        request.OrderBy = $"{orderDirection}Person.Name";
                        break;
                    default:
                        request.OrderBy = "-Id";
                        break;
                }

                var result = await customerBusiness.GetPagedResultAsync(request);
                var response = new PagedDataResponse<CustomerDTO>
                {
                    PageCount = result.PageCount,
                    PageIndex = result.PageIndex,
                    PageSize = result.PageSize,
                    RowCount = result.RowCount,
                    Results = mapper.Map<List<CustomerDTO>>(result.Results)
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
            }
        }

        [HttpPost()]
        public async Task<ActionResult<ActionResultDTO>> Add([FromBody] CustomerDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new ActionResultDTO { Message = "Datos inválidos" });

                var entity = mapper.Map<Customer>(dto);
                var result = await customerBusiness.SaveAsync(entity);
                var response = new ActionResultDTO { Code = result.ToString() };

                if (result > 0)
                {
                    response.Message = dto.Id > 0 ? "El cliente se actualizó correctamente" : "El cliente se registró correctamente";
                    return Ok(response);
                }
                else
                {
                    response.Message = "Error al registrar el cliente";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ActionResultDTO>> Delete([FromRoute] int id)
        {
            try
            {
                var customer = await customerBusiness.FindAsync(id);
                if (customer == null)
                {
                    return NotFound(new ActionResultDTO { Message = $"No se encontró un cliente con el ID {id}" });
                }

                var result = await customerBusiness.DeleteAsync(id);
                if (result)
                    return Ok(new ActionResultDTO { Code = result.ToString(), Message = "El cliente se eliminó correctamente" });
                else
                    return BadRequest(new ActionResultDTO { Message = "No se pudo eliminar el cliente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultDTO { Message = $"Error interno: {ex.Message}" });
            }
        }
    }

}
