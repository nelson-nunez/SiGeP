using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiGeP.Model.Base;

namespace SiGeP.Model.Model.Address
{
    public class Address : BaseEntity
    {
        public Address() { }

        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public int? NeighborhoodId { get; set; } // Hacer nullable si es opcional
        public virtual Neighborhood? Neighborhood { get; set; } // Hacer nullable si es opcional

        [Column(TypeName = "VARCHAR"), StringLength(64)]
        public string? NeighborhoodName { get; set; } // Usar null si NeighborhoodName es opcional

        [Column(TypeName = "VARCHAR"), StringLength(64)]
        public string StreetNumber { get; set; }

        public override string ToString()
        {
            return $"{StreetNumber}|{NeighborhoodName ?? Neighborhood?.Name}|{City?.Name}|{Province?.Name}";
        }
    }
}
