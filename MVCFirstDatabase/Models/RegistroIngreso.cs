using System;
using System.Collections.Generic;

namespace MVCFirstDatabase.Models;

public partial class RegistroIngreso
{
    public int Id { get; set; }

    public int? FkCliente { get; set; }

    public string? FkCarro { get; set; }

    public DateTime FechaHoraIngreso { get; set; }

    public int? FkParqueo { get; set; }

    public virtual Carro? FkCarroNavigation { get; set; }

    public virtual Cliente? FkClienteNavigation { get; set; }

    public virtual Parqueo? FkParqueoNavigation { get; set; }

    public virtual RegistroSalida? RegistroSalida { get; set; }
}
