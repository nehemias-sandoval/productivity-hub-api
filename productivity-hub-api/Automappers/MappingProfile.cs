using AutoMapper;
using productivity_hub_api.DTOs.Proyecto;
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
        }
    }
}
