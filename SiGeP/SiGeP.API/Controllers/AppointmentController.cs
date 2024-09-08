using AutoMapper;
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
    public class AppointmentController : Controller
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;
        private readonly AppointmentBusiness appointmentBusiness;

        public AppointmentController(IMapper mapper, IHttpContextAccessor contextAccessor, AppointmentBusiness appointmentBusiness)
        {
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.appointmentBusiness = appointmentBusiness;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAllAppointments()
        {
            var list = await appointmentBusiness.GetAsync();
            var dto = mapper.Map<IList<AppointmentDTO>>(list);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointmentById(int id)
        {
            var item = await appointmentBusiness.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var dto = mapper.Map<AppointmentDTO>(item);
            return Ok(dto);
        }

        [HttpGet("PagedData")]
        public async Task<ActionResult<PagedDataResponse<AppointmentDTO>>> GetPagedAppointments([FromQuery] PagingSortFilterRequest request)
        {
            //Fix orderBy properties out entity
            var orderDirection = "";
            if (!string.IsNullOrEmpty(request.OrderBy) && request.OrderBy[0] == '-')
            {
                orderDirection = "-";
                request.OrderBy = request.OrderBy.Substring(1);
            }
            switch (request.OrderBy)
            {
                case "Date":
                    request.OrderBy = $"{orderDirection}Date";
                    break;
                case "DoctorName":
                    request.OrderBy = $"{orderDirection}Doctor.Name";
                    break;
                default:
                    request.OrderBy = "-Id";
                    break;
            }

            var result = await appointmentBusiness.GetPagedResultAsync(request);
            var response = new PagedDataResponse<AppointmentDTO>
            {
                Results = mapper.Map<List<AppointmentDTO>>(result.Results),
                PageCount = result.PageCount,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                RowCount = result.RowCount
            };
            return Ok(response);
        }

        [HttpPost()]
        public async Task<ActionResult<ActionResultDTO>> Add([FromBody] AppointmentDTO dto)
        {
            var entity = mapper.Map<Appointment>(dto);
            entity.Payment = null;
            entity.Reminder = null;
            var result = await appointmentBusiness.AppointmentSaveAsync(entity);
            var response = new ActionResultDTO
            {
                Message = dto.Id > 0 ? "El turno se actualizó correctamente" : "El turno se registró correctamente",
                Code = result.ToString()
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ActionResultDTO>> Delete([FromRoute] int id)
        {
            var result = await appointmentBusiness.DeleteAsync(id);
            var response = new ActionResultDTO
            {
                Code = result.ToString(),
                Message = "El turno se eliminó correctamente"
            };
            return Ok(response);
        }
    }

}
