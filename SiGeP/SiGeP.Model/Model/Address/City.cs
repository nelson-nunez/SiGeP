using SiGeP.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Model.Model.Address
{
    public class City : BaseEntity
    {
        public City()
        {
        }

        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }
}
