using FluentValidation;
using productivity_hub_api.DTOs.Tarea;

namespace productivity_hub_api.Validators.Tarea
{
    public class CreateTareaValidator : AbstractValidator<CreateTareaDto>
    {
        public CreateTareaValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.FechaLimite).NotEmpty().WithMessage("El {PropertyName} es requerido");
        }
    }
}
