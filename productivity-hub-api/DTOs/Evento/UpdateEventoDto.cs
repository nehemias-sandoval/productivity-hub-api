﻿namespace productivity_hub_api.DTOs.Evento
{
    public class UpdateEventoDto
    {
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public int IdTipoEvento { get; set; }
    }
}
