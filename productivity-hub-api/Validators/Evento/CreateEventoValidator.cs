using FluentValidation;
using productivity_hub_api.DTOs.Evento;
using productivity_hub_api.DTOs.Proyecto;

namespace productivity_hub_api.Validators.Evento
{
    public class CreateEventoValidator : AbstractValidator<CreateEventoDto>
    {
        public CreateEventoValidator() 
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("El {PropertyName} es requerido");
            RuleFor(x => x.IdTipoEvento).NotNull().WithMessage("El {PropertyName} es requerido");
        }
    }
}
