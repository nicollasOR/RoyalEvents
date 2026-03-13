namespace EventsRoyalOneSirR.Applications.DTOs.EventoDTO
{
    public class CriarEventoDTO
    {
        public string Nome { get; set; } = null!;
        public string Localização { get; set; } = null!;
        public DateTime DataEvento { get; set; }

        public CriarEventoDTO()
        {

        }
    }
}
