using System;
using System.Collections.Generic;

namespace WebApiCondominio.Models;

public partial class Gasto
{
    public int Id { get; set; }

    public string Concepto { get; set; } = null!;

    public string Tipoobligacion { get; set; } = null!;

    public decimal Monto { get; set; }

    public int? Estado { get; set; }

    public virtual ICollection<Recibosdetalle> Recibosdetalles { get; } = new List<Recibosdetalle>();
}
