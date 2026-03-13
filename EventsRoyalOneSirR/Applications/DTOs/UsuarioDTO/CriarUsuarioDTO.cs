namespace EventsRoyalOneSirR.Applications.DTOs.UsuarioDTO
{
    public class CriarUsuarioDTO
    {

        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string Especialidade { get; set; } = null!;
        public CriarUsuarioDTO()
        {

        }

    }
}
