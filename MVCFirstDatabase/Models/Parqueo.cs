using System;
using System.Collections.Generic;

namespace MVCFirstDatabase.Models;

public partial class Parqueo
{
    public int Numero { get; set; }

    public string Estado { get; set; } = null!;

    public string? Tipo { get; set; }

    public virtual ICollection<RegistroIngreso> RegistroIngresos { get; set; } = new List<RegistroIngreso>();

    public virtual ICollection<RegistroSalida> RegistroSalida { get; set; } = new List<RegistroSalida>();
}
