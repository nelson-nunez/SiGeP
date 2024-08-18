using SiGeP.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Model.Model.Address
{
    public class Neighborhood : BaseEntity
    {
        public Neighborhood() { }

        public string Name {  get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
