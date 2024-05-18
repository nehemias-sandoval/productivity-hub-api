using AutoMapper;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;

namespace productivity_hub_api.Service
{
    public class TareaService : ICommonService<TareaDto, CreateTareaDto, UpdateTareaDto>
    {
        private IRepository<Tarea> _tareaRepository;
        private IMapper _mapper;
        StoreContext _context;

        public TareaService([FromKeyedServices("tareaRepository")] IRepository<Tarea> tareaRepository, IMapper mapper, StoreContext context)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<TareaDto>> GetAllAsync()
        {
            var tareas = await _tareaRepository.GetAllAsync();
            return tareas.Select(t => _mapper.Map<TareaDto>(t));
        }

        public async Task<TareaDto?> GetByIdAsync(int id)
        {
            var tarea = await _tareaRepository.GetByIdAsync(id);

            if (tarea != null)
            {
                var tareaDto = _mapper.Map<TareaDto>(tarea);

                return tareaDto;
            }

            return null;
        }

        public async Task<TareaDto> AddAsync(CreateTareaDto createTareaDto)
        {
            var tarea = _mapper.Map<Tarea>(createTareaDto);
            tarea.IdPersona = 1;

            await _tareaRepository.AddAsync(tarea);
            await _context.SaveChangesAsync();

            var tareaDto = _mapper.Map<TareaDto>(tarea);

            return tareaDto;
        }

        public async Task<TareaDto?> UpdateAsync(int id, UpdateTareaDto updateTareaDto)
        {
            var tarea = await _tareaRepository.GetByIdAsync(id);

            if(tarea != null)
            {
                tarea = _mapper.Map<UpdateTareaDto, Tarea>(updateTareaDto, tarea);

                _tareaRepository.Update(tarea);
                await _tareaRepository.SaveAsync();

                var tareaDto = _mapper.Map<TareaDto>(tarea);

                return tareaDto;        
            }

            return null;
        }

        public async Task<TareaDto?> DeleteAsync(int id)
        {
            var tarea = await _tareaRepository.GetByIdAsync(id);

            if (tarea != null)
            {
                var tareaDto = _mapper.Map<TareaDto>(tarea);

                _tareaRepository.Delete(tarea);
                await _tareaRepository.SaveAsync();

                return tareaDto;
            }

            return null;
        }
    }
}
