using productivity_hub_api.Models;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.DTOs.Notificacion
{
    public class NotificacionDto
    {
        public string Mensaje { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        public bool Leida { get; set; }

        public TipoNotificacion TipoNotificacion { get; set; }
    }
}
