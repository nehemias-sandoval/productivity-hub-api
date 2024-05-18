﻿using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.DTOs.Tarea
{
    public class TareaDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public bool Estado { get; set; }
    }
}