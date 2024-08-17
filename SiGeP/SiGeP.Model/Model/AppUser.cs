using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SiGeP.Model.Base;

namespace SiGeP.Model.Model
{
    public class AppUser : BaseEntity
    {
        [Column(TypeName = "VARCHAR"), StringLength(64)]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(128)]
        public string Password { get; set; }
    }
}
