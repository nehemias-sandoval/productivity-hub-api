using AutoMapper;
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
            CreateMap<CreateProyectoDto, Proyecto>();
            CreateMap<Proyecto, ProyectoDto>();
            CreateMap<UpdateProyectoDto, Proyecto>();

            //Tarea
            CreateMap<CreateTareaDto, Tarea>();
            CreateMap<Tarea, TareaDto>().ReverseMap();
            CreateMap<UpdateTareaDto, Tarea>();

            //Subtarea
            CreateMap<CreateSubtareaDto, Subtarea>();
            CreateMap<Subtarea, SubtareaDto>().ReverseMap();
        }
    }
}
