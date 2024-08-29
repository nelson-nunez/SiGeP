using FluentValidation;
using SiGeP.UI.Data;

namespace SiGeP.UI.Helpers
{
    public class LoginInputValidator : AbstractValidator<InputModel>
    {
        public LoginInputValidator()
        {
            RuleFor(AppRoleDTO => AppRoleDTO.UserName).NotEmpty().WithMessage("Ingrese su dirección de email.");
            RuleFor(AppRoleDTO => AppRoleDTO.Password).NotEmpty().WithMessage("Ingrese su contraseña.");
        }
    }
}
