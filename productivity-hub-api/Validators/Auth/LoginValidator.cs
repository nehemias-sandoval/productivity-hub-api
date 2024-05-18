using FluentValidation;
using productivity_hub_api.DTOs.Auth;

namespace productivity_hub_api.Validators.Auth
{
    public class LoginValidator : AbstractValidator<AuthenticateReqDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("El {PropertyName} es requerido y debe tener formato de email");
            RuleFor(x => x.Password).NotEmpty().WithMessage("El {PropertyName} es requerido");
        }
    }
}
