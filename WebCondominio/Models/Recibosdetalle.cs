using System;
using System.Collections.Generic;

namespace WebApiCondominio.Models;

public partial class Recibosdetalle
{
    public int Id { get; set; }

    public int? Idgastos { get; set; }

    public int? Idrecibo { get; set; }

    public decimal? Monto { get; set; }

    public virtual Gasto? IdgastosNavigation { get; set; }

    public virtual Recibo? IdreciboNavigation { get; set; }
}
