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
        
        [StringLength(200)]
        public string? Dirección { get; set; }
        
        [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
        [StringLength(50)]
        public string Telefono { get; set; }
        
        [EmailAddress(ErrorMessage = "Correo con formato inválido.")]
        [StringLength(150)]
        public string? Correo { get; set; }
        
        [Required(ErrorMessage = "El campo DNI de Registro es obligatorio.")]
        [RegularExpression(@"^\d{7,8}$", ErrorMessage = "DNI debe tener 7 u 8 dígitos.")]
        public string DNI { get; set; }
        
        [Required(ErrorMessage = "El Apellido es obligatorio.")]
        [StringLength(200)]
        public string? Apellido { get; set; }

        [Display(Name = "CUIT/CUIL")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "El CUIT/CUIL debe tener 11 dígitos")]
        public string? CUIT_CUIL { get; set; }
    }
}
