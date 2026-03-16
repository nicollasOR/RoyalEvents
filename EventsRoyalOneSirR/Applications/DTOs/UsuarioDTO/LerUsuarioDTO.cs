namespace EventsRoyalOneSirR.Applications.DTOs.UsuarioDTO
{
    public class LerUsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string Especialidade { get; set; } = null!;
        public bool UsuarioStatus { get; set; }
        public string TipoUsuario { get; set; } = null!;
    }
}
