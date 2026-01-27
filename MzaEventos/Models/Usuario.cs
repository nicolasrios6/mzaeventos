using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MzaEventos.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
    }

    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Ingresa un email válido.")]
        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La clave es obligatoria.")]
        public string Clave { get; set; }
        public bool Recordarme { get; set; }
    }
}
