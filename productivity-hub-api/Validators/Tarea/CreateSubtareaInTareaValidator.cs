using FluentValidation;
using productivity_hub_api.DTOs.Tarea;

namespace productivity_hub_api.Validators.Tarea
{
    public class CreateSubtareaInTareaValidator : AbstractValidator<CreateSubtareaInTareaDto>
    {
        public CreateSubtareaInTareaValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El {PropertyName} es requerido");
        }
    }
}
