using System;
using System.Collections.Generic;

namespace MVCFirstDatabase.Models;

public partial class Factura
{
    public int NoFactura { get; set; }

    public int? FkCliente { get; set; }

    public string? Nit { get; set; }

    public string? Vehiculo { get; set; }

    public decimal Total { get; set; }

    public decimal? Descuento { get; set; }

    public virtual Cliente? FkClienteNavigation { get; set; }

    public virtual RegistroSalida NoFacturaNavigation { get; set; } = null!;
}
