using EventsRoyalOneSirR.Applications.DTOs.UsuarioDTO;
using EventsRoyalOneSirR.Domains;
using EventsRoyalOneSirR.Interfaces;
using EventsRoyalOneSirR.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace EventsRoyalOneSirR.Applications.Services
{
    public class UsuarioService
    {

        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private static LerUsuarioDTO LerDTO(Usuario usuario)
        {
            LerUsuarioDTO lerDto = new LerUsuarioDTO
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Especialidade = usuario.Especialidade,
                UsuarioStatus = usuario.StatusUsuario ?? true
            };

            return lerDto;

        }

        public void conferirStatusUsuario(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);
            if (usuario.UsuarioId == null || usuario.UsuarioId == 0)
                return;

        }

        public List<LerUsuarioDTO> Listar()
        {
            List<Usuario> usuariosListar = _repository.Listar();
            List<LerUsuarioDTO> usuariosDto = usuariosListar
                .Select(usuarioQuery => LerDTO(usuarioQuery))
                .ToList();

            return usuariosDto;
        }

        private static void validarEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                throw new DomainException("Email inválido");
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha))
                throw new DomainException("Senha é obrigratória");

            using var SHA256var = SHA256.Create();
            return SHA256var.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerUsuarioDTO ObterPorId(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);

            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            return LerDTO(usuario);
        }

        public LerUsuarioDTO ObterPorEmail(string email)
        {
            Usuario? usuario = _repository.ObterPorEmail(email);

            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            return LerDTO(usuario);
        }

        private static void validarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new DomainException("Nome inválido");
        }

        public LerUsuarioDTO Adicionar(CriarUsuarioDTO criarDTO)
        {
            validarEmail(criarDTO.Email);
            validarNome(criarDTO.Nome);
            if (string.IsNullOrEmpty(criarDTO.Email))
                throw new DomainException("Usuário já existente");

            Usuario? usuario = new Usuario
            {
                Nome = criarDTO.Nome,
                Senha = HashSenha(criarDTO.Senha),
                StatusUsuario = true
            };

            _repository.Adicionar(usuario);
            return LerDTO(usuario);

        }

        public LerUsuarioDTO Atualizar(int id, CriarUsuarioDTO criarDTO)
        {
            Usuario usuarioBanco = _repository.ObterPorId(id);
            if (usuarioBanco == null)
                throw new DomainException("Usuário não encontrado");


            validarEmail(usuarioBanco.Email);
            validarNome(usuarioBanco.Nome);

            Usuario usuarioEmail = _repository.ObterPorEmail(criarDTO.Email);

            if (usuarioEmail != null && usuarioEmail.UsuarioId != id)
                throw new DomainException("Já existe um usuário com esse email");

            usuarioBanco.Nome = criarDTO.Nome;
            usuarioBanco.Especialidade = usuarioBanco.Especialidade;
            usuarioBanco.Email = usuarioBanco.Email;
            usuarioBanco.Senha = HashSenha(criarDTO.Senha);

            _repository.Atualizar(usuarioBanco);
            return LerDTO(usuarioBanco);
        }

        public LerUsuarioDTO Remover(int id)
        {
            Usuario usuario = _repository.ObterPorId(id);

            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            _repository.Remover(id);
            return LerDTO(usuario);
        }

    }
}
