using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class DoctorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly DoctorBusiness _doctorBusiness;

        public DoctorController(IMapper mapper, DoctorBusiness doctorBusiness)
        {
            _mapper = mapper;
            _doctorBusiness = doctorBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAllDoctors()
        {
            var doctors = await _doctorBusiness.GetAsync();
            var dto = _mapper.Map<IList<DoctorDTO>>(doctors);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctorById(int id)
        {
            var doctor = await _doctorBusiness.FindAsync(id);
            if (doctor == null)
                return NotFound();

            var dto = _mapper.Map<DoctorDTO>(doctor);
            return Ok(dto);
        }

        [HttpGet("PagedData")]
        public async Task<ActionResult<PagedDataResponse<DoctorDTO>>> GetPagedDoctors([FromQuery] PagingSortFilterRequest request)
        {
            // Fix orderBy properties out entity
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

            var result = await _doctorBusiness.GetPagedResultAsync(request);
            var response = new PagedDataResponse<DoctorDTO>
            {
                PageCount = result.PageCount,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                RowCount = result.RowCount,
                Results = _mapper.Map<List<DoctorDTO>>(result.Results)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ActionResultDTO>> Add([FromBody] DoctorDTO dto)
        {
            var entity = _mapper.Map<Doctor>(dto);
            var result = await _doctorBusiness.DoctorSaveAsync(entity);

            var response = new ActionResultDTO
            {
                Message = dto.Id > 0 ? "El doctor se actualizó correctamente" : "El doctor se registró correctamente",
                Code = result.ToString()
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ActionResultDTO>> Delete(int id)
        {
            var result = await _doctorBusiness.DeleteAsync(id);

            if (!result)
                return NotFound();

            var response = new ActionResultDTO
            {
                Code = result.ToString(),
                Message = "El doctor se eliminó correctamente"
            };

            return Ok(response);
        }
    }
}
