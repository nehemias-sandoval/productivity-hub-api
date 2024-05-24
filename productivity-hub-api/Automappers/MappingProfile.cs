﻿using AutoMapper;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Evento;
using productivity_hub_api.DTOs.Persona;
using productivity_hub_api.DTOs.Prioridad;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.DTOs.Subtarea;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.DTOs.TipoEvento;
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

            // Catalogos

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

            // Tipo Evento
            CreateMap<TipoEvento, TipoEventoDto>().ReverseMap();

            //Tarea
            CreateMap<CreateTareaDto, Tarea>();
            CreateMap<Tarea, TareaDto>().ReverseMap();
            CreateMap<UpdateTareaDto, Tarea>();

            //Subtarea
            CreateMap<CreateSubtareaDto, Subtarea>();
            CreateMap<Subtarea, SubtareaDto>().ReverseMap();

            // Prioridad
            CreateMap<Prioridad, PrioridadDto>().ReverseMap();
        }
    }
}
