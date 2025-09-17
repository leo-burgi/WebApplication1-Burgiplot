using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Unidad { get; set; } = null!;

    public decimal? PrecioStd { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public byte[] RowVer { get; set; } = null!;

    public int StockActual { get; set; }

    public virtual ICollection<FacturaItem> FacturaItems { get; set; } = new List<FacturaItem>();

    public virtual ICollection<OrdenItem> OrdenItems { get; set; } = new List<OrdenItem>();
}
