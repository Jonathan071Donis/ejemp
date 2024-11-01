using System;
using System.Collections.Generic;

namespace MVCFirstDatabase.Models;

public partial class Carro
{
    public string Placa { get; set; } = null!;

    public string? Color { get; set; }

    public string? Modelo { get; set; }

    public string? Tipo { get; set; }

    public int? FkCliente { get; set; }

    public virtual Cliente? FkClienteNavigation { get; set; }

    public virtual ICollection<RegistroIngreso> RegistroIngresos { get; set; } = new List<RegistroIngreso>();

    public virtual ICollection<RegistroSalida> RegistroSalida { get; set; } = new List<RegistroSalida>();
}
