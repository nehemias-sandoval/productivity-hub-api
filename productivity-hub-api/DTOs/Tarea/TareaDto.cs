﻿using productivity_hub_api.DTOs.Catalogo;
using productivity_hub_api.DTOs.Evento;
using productivity_hub_api.DTOs.Proyecto;
using productivity_hub_api.DTOs.Subtarea;

namespace productivity_hub_api.DTOs.Tarea
{
    public class TareaDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public EtiquetaDto Etiqueta { get; set; }

        public PrioridadDto Prioridad { get; set; }

        public List<SubtareaDto> Subtareas { get; set; }

        public ProyectoDto proyecto { get; set; }

        public EventoDto Evento { get; set; }
    }
}
