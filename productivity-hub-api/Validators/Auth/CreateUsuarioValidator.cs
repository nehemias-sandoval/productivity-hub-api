using FluentValidation;
using productivity_hub_api.DTOs.Auth;

namespace productivity_hub_api.Validators.Auth
{
    public class CreateUsuarioValidator : AbstractValidator<CreateUsuarioDto>
    {
        public CreateUsuarioValidator() 
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("El {PropertyName} es requerido y debe tener formato de email");
            RuleFor(x => x.Password).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Persona.Nombre).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Persona.Apellido).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Persona.FechaNacimiento).NotEmpty().WithMessage("El {PropertyName} es requerido");
        }
    }
}
