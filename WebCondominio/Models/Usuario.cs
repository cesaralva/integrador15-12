using System;
using System.Collections.Generic;

namespace WebApiCondominio.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Apellidos { get; set; }

    public string? Nombres { get; set; }

    public int? Dni { get; set; }

    public string Email { get; set; } = null!;

    public int Telefono { get; set; }

    public string? Clave { get; set; }

    public int? Estado { get; set; }

    public int? Usuarioregistra { get; set; }

    public DateTime? Fecharegistra { get; set; }

    public int? Usuarioactualiza { get; set; }

    public DateTime? Fechaactualiza { get; set; }

    public int? Idrol { get; set; }

    public int? Iddepartamento { get; set; }

    public int? CodigoVerificacion { get; set; }

    public DateTime? FechaVerificacion { get; set; }

    public virtual Departamento? IddepartamentoNavigation { get; set; }

    public virtual Role? IdrolNavigation { get; set; }

    public virtual ICollection<Incidente> Incidentes { get; } = new List<Incidente>();
}
