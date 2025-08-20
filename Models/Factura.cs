using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Factura
{
    public int Id { get; set; }

    public int OrdenId { get; set; }

    public string Numero { get; set; } = null!;

    public DateTime FechaUtc { get; set; }

    public decimal Total { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public byte[] RowVer { get; set; } = null!;

    public virtual ICollection<FacturaItem> FacturaItems { get; set; } = new List<FacturaItem>();

    public virtual Orden Orden { get; set; } = null!;
}
