using EventsRoyalOneSirR.Domains;

namespace EventsRoyalOneSirR.Interfaces
{
    public interface IUsuarioRepository
    {

        public List<Usuario> Listar();

        public Usuario? ObterPorId(int id);
        //public Usuario? ObterPorNome(string nome);
        public Usuario? ObterPorEmail(string email);
        bool EmailExiste(string email);
        void Adicionar(Usuario Usuario);
        void Atualizar(Usuario usuario);

        void Remover(int id);

    }
}

