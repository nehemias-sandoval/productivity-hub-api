using AutoMapper;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;

namespace productivity_hub_api.Service
{
    public class ProyectoService : ICommonService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto>
    {
        private IRepository<Proyecto> _proyectoRepository;
        private IMapper _mapper;

        public ProyectoService([FromKeyedServices("proyectoRepository")] IRepository<Proyecto> proyectoRepository, IMapper mapper)
        {
            _proyectoRepository = proyectoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProyectoDto>> GetAllAsync()
        {
            var proyectos = await _proyectoRepository.GetAllAsync();
            return proyectos.Select(p => _mapper.Map<ProyectoDto>(p));
        }

        public async Task<ProyectoDto?> GetByIdAsync(int id)
        {
            var proyecto = await _proyectoRepository.GetByIdAsync(id);

            if (proyecto != null)
            {
                var proyectoDto = _mapper.Map<ProyectoDto>(proyecto);

                return proyectoDto;
            }

            return null;
        }

        public async Task<ProyectoDto> AddAsync(CreateProyectoDto createProyectoDto)
        {
            var proyecto = _mapper.Map<Proyecto>(createProyectoDto);

            await _proyectoRepository.AddAsync(proyecto);
            await _proyectoRepository.SaveAsync();

            var proyectoDto = _mapper.Map<ProyectoDto>(proyecto);

            return proyectoDto;
        }

        public async Task<ProyectoDto?> UpdateAsync(int id, UpdateProyectoDto updateProyectoDto)
        {
            var proyecto = await _proyectoRepository.GetByIdAsync(id);

            if (proyecto != null)
            {
                proyecto = _mapper.Map<UpdateProyectoDto, Proyecto>(updateProyectoDto, proyecto);

                _proyectoRepository.Update(proyecto);
                await _proyectoRepository.SaveAsync();

                var proyectoDto = _mapper.Map<ProyectoDto>(proyecto);

                return proyectoDto;
            }

            return null;
        }

        public async Task<ProyectoDto?> DeleteAsync(int id)
        {
            var proyecto = await _proyectoRepository.GetByIdAsync(id);

            if (proyecto != null)
            {
                var proyectoDto = _mapper.Map<ProyectoDto>(proyecto);

                _proyectoRepository.Delete(proyecto);
                await _proyectoRepository.SaveAsync();

                return proyectoDto;
            }

            return null;
        }
    }
}
