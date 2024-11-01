using System;
using System.Collections.Generic;

namespace MVCFirstDatabase.Models;

public partial class RegistroSalida
{
    public int Id { get; set; }

    public int? FkCliente { get; set; }

    public string? FkCarro { get; set; }

    public int? FkParqueo { get; set; }

    public DateTime HoraIngreso { get; set; }

    public DateTime HoraSalida { get; set; }

    public decimal TotalHoras { get; set; }

    public int? FkTarifa { get; set; }

    public decimal? Descuento { get; set; }

    public decimal TotalCalculado { get; set; }

    public virtual Factura? Factura { get; set; }

    public virtual Carro? FkCarroNavigation { get; set; }

    public virtual Cliente? FkClienteNavigation { get; set; }

    public virtual Parqueo? FkParqueoNavigation { get; set; }

    public virtual Tarifa? FkTarifaNavigation { get; set; }

    public virtual RegistroIngreso IdNavigation { get; set; } = null!;
}
