﻿namespace productivity_hub_api.DTOs.Tarea
{
    public class CreateTareaDto
    {
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public int? IdProyecto { get; set; }

        public int? IdEvento { get; set; }

        public int IdPrioridad { get; set; }

        public List<CreateSubtareaInTareaDto> Subtareas { get; set; }
    }
}
