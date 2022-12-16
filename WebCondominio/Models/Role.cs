using System;
using System.Collections.Generic;

namespace WebApiCondominio.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Descripcionrol { get; set; } = null!;

    public int? Usuarioregistra { get; set; }

    public DateTime? Fecharegistra { get; set; }

    public int? Usuarioactualiza { get; set; }

    public DateTime? Fechaactualiza { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
