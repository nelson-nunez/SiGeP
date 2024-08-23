using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SiGeP.Business;
using SiGeP.Model.Base;
using SiGeP.Model.BaseDTO;
using SiGeP.Model.DTO;
using SiGeP.Model.Model;
using SiGeP.Model.Model.Address;


namespace SiGeP.API.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CityBusiness _cityBusiness;
        private readonly NeighborhoodBusiness _neighborhoodBusiness;
        private readonly ProvinceBusiness _provinceBusiness;


        public CityController(IMapper mapper, CityBusiness cityBusiness, NeighborhoodBusiness neighborhoodBusiness, ProvinceBusiness provinceBusiness)
        {
            _mapper = mapper;
            _cityBusiness = cityBusiness;
            _neighborhoodBusiness = neighborhoodBusiness;
            _provinceBusiness = provinceBusiness;
        }

        #region Provinces

        [HttpGet("Address/Provinces")]
        public async Task<ActionResult<IEnumerable<ProvinceDTO>>> GetAllProvinces()
        {
            var list = await _provinceBusiness.GetAsync();
            var dto = _mapper.Map<IList<ProvinceDTO>>(list);
            return Ok(dto);
        }

        #endregion

        #region Cities

        [HttpGet("Address/Cities")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetAllCities()
        {
            var list = await _cityBusiness.GetAsync();
            var dto = _mapper.Map<IList<CityDTO>>(list);
            return Ok(dto);
        }

        [HttpGet("Address/{provinceId}/Cities")]
        public async Task<ActionResult<IList<CityDTO>>> GetCitiesbyProvinceId([FromRoute] int provinceId)
        {
            var list = await _cityBusiness.GetAllCitiesbyProvince(provinceId);
            var dto = _mapper.Map<IList<CityDTO>>(list);
            return Ok(dto);
        }
        #endregion

        #region Neighborhoods

        [HttpGet("Address/Neighborhoods")]
        public async Task<ActionResult<IEnumerable<NeighborhoodDTO>>> GetAllNeighborhoods()
        {
            var list = await _neighborhoodBusiness.GetAsync();
            var dto = _mapper.Map<IList<NeighborhoodDTO>>(list);
            return Ok(dto);
        }

        [HttpGet("Address/{cityId}/Neighborhoods")]
        public async Task<ActionResult<IList<NeighborhoodDTO>>> GetNeighborhoodsbyProvinceId([FromRoute] int cityId)
        {
            var list = await _neighborhoodBusiness.GetAllNeighborhoodsbyCity(cityId);
            var dto = _mapper.Map<IList<NeighborhoodDTO>>(list);
            return Ok(dto);
        }
        #endregion
    }
}

