﻿using AutoMapper;
using productivity_hub_api.DTOs.Auth;
using productivity_hub_api.DTOs.Persona;
using productivity_hub_api.DTOs.Proyecto;
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

            // Persona
            CreateMap<CreatePersonaDto, Persona>().ReverseMap();
            CreateMap<Persona, PersonaDto>().ReverseMap();

            // Proyecto
            CreateMap<CreateProyectoDto, Proyecto>().ReverseMap();
            CreateMap<Proyecto, ProyectoDto>().ReverseMap();
            CreateMap<UpdateProyectoDto, Proyecto>().ReverseMap();
        }
    }
}
