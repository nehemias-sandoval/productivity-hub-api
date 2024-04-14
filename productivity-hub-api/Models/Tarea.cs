using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace productivity_hub_api.Models
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaLimite { get; set; }

        public bool Estado { get; set; }
    }
}
