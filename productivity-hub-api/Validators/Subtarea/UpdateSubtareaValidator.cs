using FluentValidation;
using productivity_hub_api.DTOs.Subtarea;

namespace productivity_hub_api.Validators.Subtarea
{
    public class UpdateSubtareaValidator : AbstractValidator<UpdateSubtareaDto>
    {
        public UpdateSubtareaValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Estado).NotEmpty().WithMessage("El {PropertyName} es requerido");
        }
    }
}
