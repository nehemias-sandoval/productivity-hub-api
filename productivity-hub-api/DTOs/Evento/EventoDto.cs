﻿using productivity_hub_api.DTOs.Catalogo;

namespace productivity_hub_api.DTOs.Evento
{
    public class EventoDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public TipoEventoDto TipoEvento { get; set; }
    }
}
