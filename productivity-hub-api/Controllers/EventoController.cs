﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Evento;
using productivity_hub_api.Helpers;
using productivity_hub_api.Service.EventoService;

namespace productivity_hub_api.Controllers
{
    [Route("api/v1/evento")]
    [ApiController]
    [Authorize]
    public class EventoController : ControllerBase
    {
        private IValidator<CreateEventoDto> _createEventoValidator;
        private IValidator<UpdateEventoDto> _updateEventoValidator;
        private IEventoService<EventoDto, CreateEventoDto, UpdateEventoDto> _eventoService;

        public EventoController(
            IValidator<CreateEventoDto> createEventoValidator,
            IValidator<UpdateEventoDto> updateEventoValidator,
            [FromKeyedServices("eventoService")] IEventoService<EventoDto, CreateEventoDto, UpdateEventoDto> eventoService)
        {
            _eventoService = eventoService;
            _createEventoValidator = createEventoValidator;
            _updateEventoValidator = updateEventoValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<EventoDto>> Get([FromQuery] bool? vencido) => await _eventoService.GetAllAsync(vencido);

        [HttpGet("{id}")]
        public async Task<ActionResult<EventoDto>> GetById(int id)
        {
            var proyetoDto = await _eventoService.GetByIdAsync(id);
            return proyetoDto == null ? NotFound() : Ok(proyetoDto);
        }

        [HttpPost]
        public async Task<ActionResult<EventoDto>> Add(CreateEventoDto createEventoDto)
        {
            var validationResult = await _createEventoValidator.ValidateAsync(createEventoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var eventoDto = await _eventoService.AddAsync(createEventoDto);
            if (eventoDto == null) return StatusCode(500);

            var eventoById = await _eventoService.GetByIdAsync(eventoDto.Id);
            return CreatedAtAction(nameof(GetById), new { id = eventoDto.Id }, eventoById);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EventoDto>> Update(int id, UpdateEventoDto updateEventoDto)
        {
            var validationResult = await _updateEventoValidator.ValidateAsync(updateEventoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var proyectoDto = await _eventoService.UpdateAsync(id, updateEventoDto);
            return proyectoDto == null ? NotFound() : Ok(proyectoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _eventoService.DeleteAsync(id);
            if (!result.HasValue) return NotFound();

            return result.Value ? Ok() : StatusCode(500);
        }
    }
}
