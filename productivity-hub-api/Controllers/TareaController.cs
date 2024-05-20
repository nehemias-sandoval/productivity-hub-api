using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.helpers;
using productivity_hub_api.Service;

namespace productivity_hub_api.Controllers
{
    [Route("api/v1/tarea")]
    [ApiController]
    [Authorize]
    public class TareaController : ControllerBase
    {
        private IValidator<CreateTareaDto> _createValidatorDto;
        private IValidator<UpdateTareaDto> _updateValidatorDto;
        private ICommonService<TareaDto, CreateTareaDto, UpdateTareaDto> _tareaService;

        public TareaController(
            IValidator<CreateTareaDto> createValidatorDto,
            IValidator<UpdateTareaDto> updateValidatorDto,
            [FromKeyedServices("tareaService")] ICommonService<TareaDto, CreateTareaDto, UpdateTareaDto> tareaService)
        {
            _createValidatorDto = createValidatorDto;
            _updateValidatorDto = updateValidatorDto;
            _tareaService = tareaService;
        }


        [HttpGet]
        public async Task<IEnumerable<TareaDto>> Get() => await _tareaService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TareaDto>> GetById(int id)
        {
            var tareaDto = await _tareaService.GetByIdAsync(id);
            return tareaDto == null ? NotFound() : Ok(tareaDto);
        }

        [HttpPost]
        public async Task<ActionResult<TareaDto>> Add(CreateTareaDto createTareaDto)
        {
            var validationResult = await _createValidatorDto.ValidateAsync(createTareaDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var tareaDto = await _tareaService.AddAsync(createTareaDto);
            return CreatedAtAction(nameof(GetById), new {id =  tareaDto.Id}, tareaDto);
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<TareaDto>> Update(int id, UpdateTareaDto updateTareaDto)
        {
            var validationResult = await _updateValidatorDto.ValidateAsync(updateTareaDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var tareaDto = await _tareaService.UpdateAsync(id, updateTareaDto);
            return tareaDto == null ? NotFound() : Ok(tareaDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TareaDto>> Delete(int id)
        {
            var tareaDto = await _tareaService.DeleteAsync(id);
            return tareaDto == null ? NotFound() : Ok(tareaDto); 
        }
    }
}
