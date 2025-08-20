using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Dirección { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Dni { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdateAtUtc { get; set; }


    [Timestamp] public byte[]? RowVer { get; set; } = null!;

    public virtual ICollection<Orden> Ordens { get; set; } = new List<Orden>();
}
