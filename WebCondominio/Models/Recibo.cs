using System;
using System.Collections.Generic;

namespace WebApiCondominio.Models;

public partial class Recibo
{
    public int Id { get; set; }

    public int? Iddepartamento { get; set; }

    public int Year { get; set; }

    public string Mes { get; set; } = null!;

    public decimal Montopago { get; set; }

    public DateTime? Fechavencimiento { get; set; }

    public string? Estadopago { get; set; }

    public virtual Departamento? IddepartamentoNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; } = new List<Pago>();

    public virtual ICollection<Recibosdetalle> Recibosdetalles { get; } = new List<Recibosdetalle>();
}
