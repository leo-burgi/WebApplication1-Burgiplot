using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class Orden
{
    public int Id { get; set; }
    
    [Display(Name = "Cliente")]
    public int ClienteId { get; set; }
   
    [Display(Name = "Fecha")]
    public DateTime FechaUtc { get; set; }

    [Required, Display(Name = "Estado actual")]
    public string Estado { get; set; } = null!;

    public string? Observaciones { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public byte[] RowVer { get; set; } = null!;

    [DataType(DataType.Currency)]
    public decimal Total { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Factura? Factura { get; set; }

    public virtual ICollection<OrdenItem> OrdenItems { get; set; } = new List<OrdenItem>();
}
