using FluentValidation;
using productivity_hub_api.DTOs.Proyecto;

namespace productivity_hub_api.Validators.Proyecto
{
    public class CreateProyectoValidator : AbstractValidator<CreateProyectoDto>
    {
        public CreateProyectoValidator() 
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("El {PropertyName} es requerido");
        }
    }
}
