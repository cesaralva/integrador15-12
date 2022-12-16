using System;
using System.Collections.Generic;

namespace WebApiCondominio.Models;

public partial class Pago
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public string? Ruta { get; set; }

    public int? Idrecibo { get; set; }

    public DateTime? Fechapago { get; set; }

    public virtual Recibo? IdreciboNavigation { get; set; }
}
