using EventsRoyalOneSirR.Domains;

namespace EventsRoyalOneSirR.Interfaces
{
    public interface IEventoRepository
    {
        public List<Evento> Listar();

        public Evento? ObterPorId(int id);
        public Evento? ObterPorNome(string nome);

        public bool EventoExiste(string nome);
        public void Adicionar(Evento evento);
        public void Atualizar(Evento evento);
        public void Remover(int id);
    }
}
