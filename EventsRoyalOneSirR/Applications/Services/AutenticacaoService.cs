using EventsRoyalOneSirR.Applications;
using EventsRoyalOneSirR.Applications.DTOs.AutenticacaoDTO;
using EventsRoyalOneSirR.Domains;
using EventsRoyalOneSirR;
using EventsRoyalOneSirR.Interfaces;
using EventsRoyalOneSirR.Applications.Autenticacao;
using EventsRoyalOneSirR.Exceptions;


namespace EventsRoyalOneSirR.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJWTT _tokenJWT;

        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJWTT tokenJWT)
        {
            _repository = repository;
            _tokenJWT = tokenJWT;
        }

        //compara a hash

        private static bool verificarSenha(string senhaDigitada, byte[] SenhaHashBanco)
        {
            using var sha =
                System.Security.Cryptography.SHA256.Create();

            var hashDigitado = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

            return hashDigitado.SequenceEqual(SenhaHashBanco);
        }

        public TokenDTO Login(LoginDTO _loginDto)
        {
            Usuario usuario = _repository.ObterPorEmail(_loginDto.Email);

            if (usuario == null)
                throw new DomainException("Email ou senha inválidos.");

            //comparar a senha digitada com a senha armazenada

            if (!verificarSenha(_loginDto.Senha, usuario.Senha))
                throw new DomainException("Email ou senha inválidos.");

            //gerando o token

            var token = _tokenJWT.gerarToken(usuario);

            TokenDTO novoToken = new TokenDTO { Token = token };

            return novoToken;


        }

    }
}
