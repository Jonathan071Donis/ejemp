using System;
using System.Collections.Generic;

namespace MVCFirstDatabase.Models;

public partial class Tarifa
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int MinimoDeHoras { get; set; }

    public decimal PrecioPorHora { get; set; }

    public virtual ICollection<RegistroSalida> RegistroSalida { get; set; } = new List<RegistroSalida>();
}
