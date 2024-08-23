using AutoMapper;
using SiGeP.Model.DTO;
using SiGeP.Model.Model;
using SiGeP.Model.Model.Address;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SiGeP.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeo para Customer y CustomerDto
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.PersonLastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(dest => dest.PersonDNI, opt => opt.MapFrom(src => src.Person.DNI))
                .ForMember(dest => dest.PersonBirthDate, opt => opt.MapFrom(src => src.Person.BirthDate))
                .ForMember(dest => dest.PersonPhone, opt => opt.MapFrom(src => src.Person.Phone))
                .ForMember(dest => dest.PersonEmail, opt => opt.MapFrom(src => src.Person.Email))
                .ForMember(dest => dest.PersonAge, opt => opt.MapFrom(src => src.Person.Age))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.Person.GenderId))
                .ForMember(dest => dest.ProvinceId, opt => opt.MapFrom(src => src.Person.Address.ProvinceId))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Person.Address.CityId))
                .ForMember(dest => dest.NeighborhoodId, opt => opt.MapFrom(src => src.Person.Address.NeighborhoodId))
                .ForMember(dest => dest.StreetNumber, opt => opt.MapFrom(src => src.Person.Address.StreetNumber))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ReverseMap();

            // Mapeo para Doctor y DoctorDto
            CreateMap<Doctor, DoctorDTO>()
                .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.PersonLastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(dest => dest.PersonDNI, opt => opt.MapFrom(src => src.Person.DNI))
                .ForMember(dest => dest.PersonBirthDate, opt => opt.MapFrom(src => src.Person.BirthDate))
                .ForMember(dest => dest.PersonPhone, opt => opt.MapFrom(src => src.Person.Phone))
                .ForMember(dest => dest.PersonEmail, opt => opt.MapFrom(src => src.Person.Email))
                .ForMember(dest => dest.PersonAge, opt => opt.MapFrom(src => src.Person.Age))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.Person.GenderId))
                .ForMember(dest => dest.ProvinceId, opt => opt.MapFrom(src => src.Person.Address.ProvinceId))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Person.Address.CityId))
                .ForMember(dest => dest.NeighborhoodId, opt => opt.MapFrom(src => src.Person.Address.NeighborhoodId))
                .ForMember(dest => dest.StreetNumber, opt => opt.MapFrom(src => src.Person.Address.StreetNumber))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty))
                .ReverseMap();


            // Mapeo para Gender y GenderDTO
            CreateMap<Gender, GenderDTO>().ReverseMap();

            //Address
            CreateMap<Province, ProvinceDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<Neighborhood, NeighborhoodDTO>().ReverseMap();

        }
    }
}
