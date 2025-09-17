using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class OrdenItem
{
    public int Id { get; set; }

    public int OrdenId { get; set; }

    public int MaterialId { get; set; }

    public decimal Cantidad { get; set; }

    public decimal PrecioUnit { get; set; }

    public decimal? Total { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual Orden Orden { get; set; } = null!;
}
