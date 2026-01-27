using Microsoft.AspNetCore.Mvc.Rendering;
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
        public bool? Destacado { get; set; }
        public bool? Activo { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string UrlImagen { get; set; }
    }

    public class EventoCreateViewModel
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "El título no puede exceder los 200 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha y hora son obligatorias")]
        [Display(Name = "Fecha y Hora")]
        [DataType(DataType.DateTime)]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria")]
        [StringLength(300)]
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "Seleccione una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        [Url(ErrorMessage = "Ingrese una URL válida")]
        [Display(Name = "Link de Entradas")]
        public string? LinkEntradas { get; set; }

        [Display(Name = "Evento Destacado")]
        public bool Destacado { get; set; }

        [Display(Name = "Imagen del Evento")]
        public IFormFile? Imagen { get; set; }
        public string? UrlImagen { get; set; }

        //// Para popular el dropdown de categorías
        //public List<SelectListItem>? Categorias { get; set; }
    }
}
