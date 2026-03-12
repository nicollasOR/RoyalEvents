using System;
using System.Collections.Generic;

namespace EventsRoyalOneSir.Domains;

public partial class TipoUsuario
{
    public int TipoUsuarioId { get; set; }

    public string Tipo_de_Usuario { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
