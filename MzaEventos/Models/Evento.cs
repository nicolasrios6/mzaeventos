using System.ComponentModel.DataAnnotations;

namespace MzaEventos.Models
{
    public class Evento
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }
        [Required]

        public string Descripcion { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FechaHora { get; set; }
        public string Ubicacion { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        [Url]
        public string LinkEntradas { get; set; }
        public bool Destacado { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string UrlImagen { get; set; }
    }
}
