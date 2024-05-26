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
            RuleFor(x => x.FechaLimite)
                .NotEmpty().WithMessage("El {PropertyName} es requerido")
                .GreaterThan(DateTime.Today).WithMessage("La {PropertyName} debe ser mayor al día actual");
            RuleFor(x => x.IdProyectoOrEvento).NotNull().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.IsProyecto).NotNull().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Subtareas).NotEmpty().WithMessage("La tarea debe contener por lo menos una subtarea");
            RuleForEach(x => x.Subtareas).SetValidator(new CreateSubtareaInTareaValidator());
        }
    }
}
