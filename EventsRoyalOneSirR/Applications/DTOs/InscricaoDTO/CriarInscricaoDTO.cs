namespace EventsRoyalOneSirR.Applications.DTOs.InscricaoDTO
{
    public class CriarInscricaoDTO
    {
        public int InscricaoId { get; set; }
        public int UsuarioId { get; set; }
        public int EventoId { get; set; }
        public DateTime dataInscricao { get; set; }


    }
}
