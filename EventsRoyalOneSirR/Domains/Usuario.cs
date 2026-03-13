using System;
using System.Collections.Generic;

namespace EventsRoyalOneSirR.Domains;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public int? TipoUsuario { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public string? Especialidade { get; set; }

    public bool? StatusUsuario { get; set; }

    public virtual ICollection<Inscrição> Inscrição { get; set; } = new List<Inscrição>();

    public virtual TipoUsuario? TipoUsuarioNavigation { get; set; }
}
