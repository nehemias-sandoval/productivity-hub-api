using AutoMapper;
using productivity_hub_api.DTOs.Subtarea;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;

namespace productivity_hub_api.Service
{
    public class SubtareaService : ICommonService<SubtareaDto, CreateSubtareaDto, UpdateSubtareaDto>
    {
        private IRepository<Subtarea> _repositorySubtarea;
        private IMapper _mapper;

        public SubtareaService([FromKeyedServices("subtareaRepository")] IRepository<Subtarea> repositorySubtarea, IMapper mapper)
        {
            _repositorySubtarea = repositorySubtarea;
            _mapper = mapper;
        }


        public async Task<IEnumerable<SubtareaDto>> GetAllAsync()
        {
            var subtareas = await _repositorySubtarea.GetAllAsync();
            return subtareas.Select(s => _mapper.Map<SubtareaDto>(s));
        }

        public async Task<SubtareaDto?> GetByIdAsync(int id)
        {
            var subtarea = await _repositorySubtarea.GetByIdAsync(id);

            if(subtarea != null)
            {
                var subtareaDto = _mapper.Map<SubtareaDto>(subtarea);

                return subtareaDto;
            }

            return null;
        }

        public async Task<SubtareaDto> AddAsync(CreateSubtareaDto createSubtareaDto)
        {
            var subtarea = _mapper.Map<Subtarea>(createSubtareaDto);

            await _repositorySubtarea.AddAsync(subtarea);
            await _repositorySubtarea.SaveAsync();

            var subtareaDto = _mapper.Map<SubtareaDto>(subtarea);

            return subtareaDto;
        }

        public async Task<SubtareaDto?> UpdateAsync(int id, UpdateSubtareaDto updateSubtareaDto)
        {
            var subtarea = await _repositorySubtarea.GetByIdAsync(id);

            if (subtarea != null)
            {
                subtarea = _mapper.Map<UpdateSubtareaDto, Subtarea>(updateSubtareaDto, subtarea);

                _repositorySubtarea.Update(subtarea);
                await _repositorySubtarea.SaveAsync();

                var subtareaDto = _mapper.Map<SubtareaDto>(subtarea);

                return subtareaDto;
            }

            return null;
        }

        public async Task<SubtareaDto?> DeleteAsync(int id)
        {
            var subtarea = await _repositorySubtarea.GetByIdAsync(id);

            if(subtarea != null)
            {
                var subtaraDto = _mapper.Map<SubtareaDto>(subtarea);

                _repositorySubtarea.Delete(subtarea);
                await _repositorySubtarea.SaveAsync();

                return subtaraDto;
            }

            return null;
        }

    }
}
