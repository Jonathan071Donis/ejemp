using System;
using System.Collections.Generic;

namespace MVCFirstDatabase.Models;

public partial class Cliente
{
    public int NoIdentificacion { get; set; }

    public string Nit { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Carro> Carros { get; set; } = new List<Carro>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<RegistroIngreso> RegistroIngresos { get; set; } = new List<RegistroIngreso>();

    public virtual ICollection<RegistroSalida> RegistroSalida { get; set; } = new List<RegistroSalida>();
}
