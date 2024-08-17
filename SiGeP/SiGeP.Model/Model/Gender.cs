﻿using SiGeP.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiGeP.Model.Model
{
    public class Gender : BaseEntity
    {
        public const int Male = 1;
        public const int Female = 2;
        public const int Other = 3;

        [Column(TypeName = "VARCHAR"), StringLength(128)]
        public string Name { get; set; }
    }
}
