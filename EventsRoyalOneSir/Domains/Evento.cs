using System;
using System.Collections.Generic;

namespace EventsRoyalOneSir.Domains;

public partial class Evento
{
    public int EventoId { get; set; }

    public string Nome { get; set; } = null!;

    public DateTime DataEvento { get; set; }

    public string Localizacao { get; set; } = null!;

    public virtual ICollection<Inscrição> Inscrição { get; set; } = new List<Inscrição>();
}
