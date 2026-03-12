using System;
using System.Collections.Generic;

namespace EventsRoyalOneSir.Domains;

public partial class Inscrição
{
    public int InscriçãoId { get; set; }

    public int UsuarioId { get; set; }

    public int EventoId { get; set; }

    public DateTime? DataInscrição { get; set; }

    public virtual Evento Evento { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
