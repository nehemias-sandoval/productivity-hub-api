using AutoMapper;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.DTOs.Subtarea;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Service.ProyectoService;

namespace productivity_hub_api.Service.TareaService
{
    public class SubtareaService : ISubtareaService<SubtareaDto, CreateSubtareaDto, UpdateSubtareaDto>
    {
        private IRepository<Subtarea> _repositorySubtarea;
        private IMapper _mapper;
        private ITareaService<TareaDto, CreateTareaDto, UpdateTareaDto, ChangeEtiquetaTareaDto> _tareaService;
        private IProyectoService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto> _proyectoService;

        public SubtareaService(
            [FromKeyedServices("subtareaRepository")] IRepository<Subtarea> repositorySubtarea,
            IMapper mapper,
            [FromKeyedServices("tareaService")] ITareaService<TareaDto, CreateTareaDto, UpdateTareaDto, ChangeEtiquetaTareaDto> tareaService,
            [FromKeyedServices("proyectoService")] IProyectoService<ProyectoDto, CreateProyectoDto, UpdateProyectoDto> proyectoService)
        {
            _repositorySubtarea = repositorySubtarea;
            _mapper = mapper;
            _tareaService = tareaService;
            _proyectoService = proyectoService;
        }

        public async Task<IEnumerable<SubtareaDto>> GetAllAsync(bool? pendientes, int idTarea)
        {
            var subtareas = await _repositorySubtarea.GetAllAsync();

            if (pendientes.HasValue)
                return subtareas.Where(st => st.Estado == pendientes.Value && st.IdTarea == idTarea).Select(st => _mapper.Map<SubtareaDto>(st));

            return subtareas.Where(st => st.IdTarea == idTarea).Select(st => _mapper.Map<SubtareaDto>(st));
        }

        public async Task<SubtareaDto?> GetByIdAsync(int id)
        {
            var subtarea = await _repositorySubtarea.GetByIdAsync(id);

            if (subtarea != null)
            {
                var subtareaDto = _mapper.Map<SubtareaDto>(subtarea);

                return subtareaDto;
            }

            return null;
        }

        public async Task<SubtareaDto?> AddAsync(CreateSubtareaDto createSubtareaDto)
        {
            var subtarea = _mapper.Map<Subtarea>(createSubtareaDto);
            await _repositorySubtarea.AddAsync(subtarea);
            await _repositorySubtarea.SaveAsync();

            var subtareaDto = _mapper.Map<SubtareaDto>(subtarea);

            await _tareaService.CompletarWhenSubtareasAreCompletadasAsync(subtarea.IdTarea);

            var subtareaById = await _repositorySubtarea.GetByIdAsync(subtarea.Id);
            if (subtareaById != null && subtareaById.Tarea.ProyectoTareas.Count() > 0)
                await _proyectoService.ChangeEstadoAsync(subtareaById.Tarea.ProyectoTareas.Select(pt => pt.Proyecto).First().Id);

            return subtareaDto;
        }

        public async Task<SubtareaDto?> UpdateAsync(int id, UpdateSubtareaDto updateSubtareaDto)
        {
            var subtarea = await _repositorySubtarea.GetByIdAsync(id);

            if (subtarea != null)
            {
                subtarea = _mapper.Map(updateSubtareaDto, subtarea);

                _repositorySubtarea.Update(subtarea);
                await _repositorySubtarea.SaveAsync();

                var subtareaDto = _mapper.Map<SubtareaDto>(subtarea);

                return subtareaDto;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subtarea = await _repositorySubtarea.GetByIdAsync(id);

            if (subtarea != null)
            {
                var tarea = subtarea.Tarea;
                var proyecto = tarea.ProyectoTareas.Select(pt => pt.Proyecto).FirstOrDefault();
                _repositorySubtarea.Delete([subtarea]);
                await _repositorySubtarea.SaveAsync();

                await _tareaService.CompletarWhenSubtareasAreCompletadasAsync(subtarea.IdTarea);
                if (proyecto != null) await _proyectoService.ChangeEstadoAsync(proyecto.Id);

                return true;
            }

            return false;
        }

        public async Task<SubtareaDto?> ChangeStateAsync(int id)
        {
            var subtarea = await _repositorySubtarea.GetByIdAsync(id);

            if (subtarea != null)
            {
                subtarea = _mapper.Map<Subtarea>(subtarea);
                subtarea.Estado = subtarea.Estado ? false : true;

                _repositorySubtarea.Update(subtarea);
                await _repositorySubtarea.SaveAsync();

                var subtareaDto = _mapper.Map<SubtareaDto>(subtarea);

                await _tareaService.CompletarWhenSubtareasAreCompletadasAsync(subtarea.IdTarea);
                await _proyectoService.ChangeEstadoAsync(subtarea.Tarea.ProyectoTareas.Select(pt => pt.Proyecto).First().Id);

                return subtareaDto;
            }

            return null;
        }
    }
}
