using System;
using System.Collections.Generic;

namespace WebApiCondominio.Models;

public partial class Departamento
{
    public int Id { get; set; }

    public string Torre { get; set; } = null!;

    public int Piso { get; set; }

    public int Numero { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? Activo { get; set; }

    public int? Usuarioregistra { get; set; }

    public DateTime? Fecharegistra { get; set; }

    public int? Usuarioactualiza { get; set; }

    public DateTime? Fechaactualiza { get; set; }

    public virtual ICollection<Recibo> Recibos { get; } = new List<Recibo>();

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
