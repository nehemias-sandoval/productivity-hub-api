﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using productivity_hub_api.DTOs.Subtarea;
using productivity_hub_api.DTOs.Tarea;
using productivity_hub_api.Service;

namespace productivity_hub_api.Controllers
{
    [ApiController]
    [Route("api/v1/subtarea")]
    public class SubtareaController : ControllerBase
    {
        IValidator<CreateSubtareaDto> _createSubtareaValidator;
        IValidator<UpdateSubtareaDto> _updateSubtareaValidator;
        ICommonService<SubtareaDto, CreateSubtareaDto,  UpdateSubtareaDto> _subtareaService;

        public SubtareaController(
            IValidator<CreateSubtareaDto> createSubtareaValidator,
            IValidator<UpdateSubtareaDto> updateSubtareaValidator,
            [FromKeyedServices("subtareaService")] ICommonService<SubtareaDto, CreateSubtareaDto, UpdateSubtareaDto> subtareaService)
        {
            _createSubtareaValidator = createSubtareaValidator;
            _updateSubtareaValidator = updateSubtareaValidator;
            _subtareaService = subtareaService;
        }

        [HttpGet]
        public async Task<IEnumerable<SubtareaDto>> Get() => await _subtareaService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<SubtareaDto>> GetById(int id)
        {
            var subtareaDto = await _subtareaService.GetByIdAsync(id);
            return subtareaDto == null ? NotFound() : Ok(subtareaDto);
        }

        [HttpPost]
        public async Task<ActionResult<SubtareaDto>> Add(CreateSubtareaDto createSubtareaDto)
        {
            var validationResult = await _createSubtareaValidator.ValidateAsync(createSubtareaDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var subtareaDto = await _subtareaService.AddAsync(createSubtareaDto);
            return CreatedAtAction(nameof(GetById), new { id = subtareaDto.Id }, subtareaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SubtareaDto>> Update(int id, UpdateSubtareaDto updateSubtareaDto)
        {
            var validationResult = await _updateSubtareaValidator.ValidateAsync(updateSubtareaDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var subtareaDto = await _subtareaService.UpdateAsync(id, updateSubtareaDto);
            return subtareaDto == null ? NotFound() : Ok(subtareaDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SubtareaDto>> Delete(int id)
        {
            var subtareaDto = await _subtareaService.DeleteAsync(id);
            return subtareaDto == null ? NotFound() : Ok(subtareaDto);
        }

    }
}