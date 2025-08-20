using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class VwFacturacionMensual
{
    public int? Año { get; set; }

    public int? Mes { get; set; }

    public decimal? ImporteMes { get; set; }
}
