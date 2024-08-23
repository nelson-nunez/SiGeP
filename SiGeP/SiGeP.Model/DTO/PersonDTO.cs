using SiGeP.Model.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Model.DTO
{
    public  class PersonDTO: BaseEntityDTO<int>
    {
        //Person
        public string PersonName { get; set; }
        public string PersonLastName { get; set; }
        public string PersonDNI { get; set; }
        public DateTime PersonBirthDate { get; set; }
        public string PersonPhone { get; set; }
        public string PersonEmail { get; set; }
        public int PersonAge { get; set; }
        public int GenderId { get; set; }

        //Address
        public int ProvinceId { get; set; }     
        public int CityId { get; set; }
        public int? NeighborhoodId { get; set; }
        public string StreetNumber { get; set; }
    }
}
