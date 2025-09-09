using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class FacturaItem
{
    public int Id { get; set; }

    public int FacturaId { get; set; }

    public int MaterialId { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Cantidad { get; set; }

    public decimal PrecioUnit { get; set; }

    public virtual Factura Factura { get; set; } = null!;

    public virtual Material Material { get; set; } = null!;
}
