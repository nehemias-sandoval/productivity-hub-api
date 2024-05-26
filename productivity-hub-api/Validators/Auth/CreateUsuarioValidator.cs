using FluentValidation;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.Repository.AuthRepository;
using productivity_hub_api.Service.AuthService;

namespace productivity_hub_api.Validators.Auth
{
    public class CreateUsuarioValidator : AbstractValidator<CreateUsuarioDto>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CreateUsuarioValidator(IUsuarioRepository usuarioRepository) 
        {
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("El {PropertyName} es requerido y debe tener formato de email")
                 .MustAsync(BeUniqueEmail).WithMessage("El {PropertyName} ya existe. Por favor ingrese un email válido");
            RuleFor(x => x.Password).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Persona.Nombre).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Persona.Apellido).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Persona.FechaNacimiento)
                .NotEmpty().WithMessage("El {PropertyName} es requerido")
                .LessThan(DateTime.Today).WithMessage("La {PropertyName} no puede ser igual o mayor al día actual");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _usuarioRepository.EmailExistsAsync(email, cancellationToken);
        }
    }
}
