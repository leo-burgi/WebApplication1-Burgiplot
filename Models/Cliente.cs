using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }
        public string Dirección { get; set; }
        
        [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
        public string Telefono { get; set; }
        public string Correo { get; set; }
        
        [Required(ErrorMessage = "El campo DNI de Registro es obligatorio.")]
        public string DNI { get; set; }

        public string? Apellido { get; set; }

        [Display(Name = "CUIT/CUIL")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "El CUIT/CUIL debe tener 11 dígitos")]
        public string? CUIT_CUIL { get; set; }
    }
}
