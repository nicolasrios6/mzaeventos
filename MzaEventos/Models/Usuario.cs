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
}
