using System.ComponentModel.DataAnnotations;

namespace SiGeP.UI.Data
{
    public class InputModel
    {
        [Required(ErrorMessage = "Nombre de usuario requerido")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
        //public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "¿Recordar mi contraseña?")]
        public bool RememberMe { get; set; }
    }
}
