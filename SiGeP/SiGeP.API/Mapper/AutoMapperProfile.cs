using AutoMapper;
using SiGeP.Model.DTO;
using SiGeP.Model.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SiGeP.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();

            CreateMap<Gender, GenderDTO>().ReverseMap();
        }
    }
}
