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
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("El {PropertyName} es requerido")
                .GreaterThan(DateTime.Today).WithMessage("La {PropertyName} debe ser mayor al día actual");
            RuleFor(x => x.IdTipoEvento).NotNull().WithMessage("El {PropertyName} es requerido");
        }
    }
}
