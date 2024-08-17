using SiGeP.Model.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Model.DTO
{
    public class CustomerDTO : BaseEntityDTO<int>
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string CUIL { get; set; }

        public int GenderId { get; set; }

        public string Phone { get; set; }

        public int Age { get; set; }
    }
}