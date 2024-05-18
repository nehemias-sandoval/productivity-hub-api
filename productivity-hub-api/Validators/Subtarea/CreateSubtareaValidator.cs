using FluentValidation;
using productivity_hub_api.DTOs.Subtarea;

namespace productivity_hub_api.Validators.Subtarea
{
    public class CreateSubtareaValidator : AbstractValidator<CreateSubtareaDto>
    {
        public CreateSubtareaValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El {PropertyName} es requerido");
        }
    }
}
