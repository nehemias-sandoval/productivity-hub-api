﻿using productivity_hub_api.DTOs.Persona;
using productivity_hub_api.DTOs.Prioridad;
using productivity_hub_api.DTOs.Subtarea;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.DTOs.Tarea
{
    public class TareaDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public PrioridadDto Prioridad { get; set; }

        public List<SubtareaDto> Subtareas { get; set; }
    }
}
