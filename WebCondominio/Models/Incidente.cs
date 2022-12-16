using System;
using System.Collections.Generic;

namespace WebApiCondominio.Models;

public partial class Incidente
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public string? Detalle { get; set; }

    public DateTime? Fecha { get; set; }

    public byte[]? Documento { get; set; }

    public int? Usuarioregistra { get; set; }

    public DateTime? Fecharegistra { get; set; }

    public int? Usuarioactualiza { get; set; }

    public DateTime? Fechaactualiza { get; set; }

    public int? Idusuario { get; set; }

    public string? Estado { get; set; }

    public string? Observacion { get; set; }

    public virtual Usuario? IdusuarioNavigation { get; set; }
}
