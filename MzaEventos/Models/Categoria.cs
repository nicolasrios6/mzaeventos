using System.ComponentModel.DataAnnotations;

namespace MzaEventos.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        public ICollection<Evento> Eventos { get; set; }
    }
}
