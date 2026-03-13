using EventsRoyalOneSirR.Interfaces;
using EventsRoyalOneSirR.Domains;
using EventsRoyalOneSirR.Contexts;
namespace EventsRoyalOneSirR.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EventsRoyalOneSirRContext _context;

        public UsuarioRepository(EventsRoyalOneSirRContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }


        public Usuario? ObterPorId(int id)
        {
            return _context.Usuario.Find(id);
        }

        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(emailAux => emailAux.Email == email);
        }


        public bool EmailExiste(string email)
        {
            return _context.Usuario.Any(emailL => emailL.Email == email);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();

        }

        public void Atualizar(Usuario usuario)
        {
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(usuarioAux => usuarioAux.UsuarioId == usuario.UsuarioId);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Senha = usuario.Senha;
            usuarioBanco.Especialidade = usuario.Especialidade;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(usuarioAux => usuarioAux.UsuarioId == id);
            if (usuarioBanco == null)
                return;

            _context.Usuario.Remove(usuarioBanco);
            _context.SaveChanges();

        }

    }
}
