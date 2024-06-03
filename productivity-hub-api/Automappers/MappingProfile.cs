using AutoMapper;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Catalogo;
using productivity_hub_api.DTOs.Configuracion;
using productivity_hub_api.DTOs.Evento;
using productivity_hub_api.DTOs.Notificacion;
using productivity_hub_api.DTOs.Persona;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.DTOs.Subtarea;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Models;

namespace productivity_hub_api.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Usuario
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<CreateUsuarioDto, Usuario>().ReverseMap();
            CreateMap<Usuario, AuthenticateResDto>().ReverseMap();

            // Configuracion
            CreateMap<Configuracion, ConfiguracionDto>().ReverseMap();

            // Catalogos
            CreateMap<Etiqueta, EtiquetaDto>().ReverseMap();
            CreateMap<Prioridad,  PrioridadDto>().ReverseMap(); 
            CreateMap<TipoEvento, TipoEventoDto>().ReverseMap();
            CreateMap<TipoNotificacion, TipoNotificacionDto>().ReverseMap();

            // Persona
            CreateMap<CreatePersonaDto, Persona>().ReverseMap();
            CreateMap<Persona, PersonaDto>().ReverseMap();

            // Proyecto
            CreateMap<CreateProyectoDto, Proyecto>().ReverseMap();
            CreateMap<Proyecto, ProyectoDto>().ReverseMap();
            CreateMap<UpdateProyectoDto, Proyecto>().ReverseMap();

            // Evento
            CreateMap<CreateEventoDto, Evento>().ReverseMap();
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<UpdateEventoDto, Evento>().ReverseMap();

            //Tarea
            CreateMap<CreateTareaDto, Tarea>();
            CreateMap<Tarea, TareaDto>().ReverseMap();
            CreateMap<CreateSubtareaInTareaDto, Subtarea>();
            CreateMap<UpdateTareaDto, Tarea>();

            //Subtarea
            CreateMap<CreateSubtareaDto, Subtarea>();
            CreateMap<UpdateSubtareaDto, Subtarea>();
            CreateMap<Subtarea, SubtareaDto>().ReverseMap();

            //Notificaciones
            CreateMap<Notificacion, NotificacionDto>().ReverseMap();
        }
    }
}
