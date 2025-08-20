using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class VwOrdenesMensuale
{
    public int? Año { get; set; }

    public int? Mes { get; set; }

    public int? OrdenesTotales { get; set; }

    public int? OrdenesEfectivas { get; set; }
}
