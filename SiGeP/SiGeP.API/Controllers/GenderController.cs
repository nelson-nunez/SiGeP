using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGeP.Business;
using SiGeP.Model.DTO;

namespace SiGeP.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class GenderController : Controller
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;
        private readonly GenderBusiness genderBusiness;

        public GenderController(IMapper mapper, IHttpContextAccessor contextAccessor, GenderBusiness genderBusiness)
        {
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
            this.genderBusiness = genderBusiness;
        }

        [HttpGet()]
        public async Task<ActionResult<GenderDTO>> GetAllGenders()
        {
            var list = await genderBusiness.GetAsync();
            var dto = mapper.Map<IList<GenderDTO>>(list);
            return Ok(dto);
        }
    }
}
