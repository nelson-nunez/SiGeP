﻿using SiGeP.Model.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Model.DTO
{
    public class CustomerDTO : PersonDTO
    {
        public int DoctorId { get; set; }

    }
}

