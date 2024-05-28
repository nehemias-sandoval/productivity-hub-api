using FluentValidation;
using productivity_hub_api.DTOs.Evento;
using productivity_hub_api.DTOs.Proyecto;

namespace productivity_hub_api.Validators.Proyecto
{
    public class UpdateEventoValidator : AbstractValidator<UpdateEventoDto>
    {
        public UpdateEventoValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.FechaInicio)
                .NotEmpty().WithMessage("El {PropertyName} es requerido")
                .GreaterThan(DateTime.Now).WithMessage("La {PropertyName} debe ser mayor al día actual");
            RuleFor(x => x.FechaFin)
                .NotEmpty().WithMessage("El {PropertyName} es requerido")
                .GreaterThan(x => x.FechaInicio).WithMessage("La {PropertyName} debe ser mayor a la fecha de inicio");
            RuleFor(x => x.IdTipoEvento).NotNull().WithMessage("El {PropertyName} es requerido");
        }
    }
}
