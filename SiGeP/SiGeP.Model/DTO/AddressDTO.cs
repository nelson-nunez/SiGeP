using SiGeP.Model.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Model.DTO
{
    public class CityDTO : BaseEntityDTO<int>
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
    }

    public class NeighborhoodDTO : BaseEntityDTO<int>
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
    }

    public class ProvinceDTO : BaseEntityDTO<int>
    {
        public string Name { get; set; }
    }
}

