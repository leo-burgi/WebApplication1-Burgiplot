using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class StockMovimiento
{
    public long Id { get; set; }

    public int MaterialId { get; set; }

    public decimal Cantidad { get; set; }

    public string Motivo { get; set; } = null!;

    public string? RefTipo { get; set; }

    public int? RefId { get; set; }

    public DateTime FechaUtc { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Material Material { get; set; } = null!;
}
