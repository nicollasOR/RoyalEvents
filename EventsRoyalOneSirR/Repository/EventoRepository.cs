using EventsRoyalOneSirR.Contexts;
using EventsRoyalOneSirR.Domains;
using EventsRoyalOneSirR.Interfaces;

namespace EventsRoyalOneSirR.Repository
{
    public class EventoRepository : IEventoRepository
    {

        private readonly EventsRoyalOneSirRContext _context;
        public EventoRepository(EventsRoyalOneSirRContext context)
        {
            _context = context;
        }

        public List<Evento> Listar()
        {
            return _context.Evento.ToList();
        }

        public Evento? ObterPorId(int id)
        {
            return _context.Evento.Find(id);
        }

        public Evento? ObterPorNome(string nome)
        {
            return _context.Evento.Find(nome);
        }

        public bool EventoExiste(string nome)
        {
            return _context.Evento.Any(nomeEvento => nomeEvento.Nome == nome);
        }

        public void Adicionar(Evento evento)
        {
            _context.Evento.Add(evento);
            _context.SaveChanges();

        }

        public void Atualizar(Evento evento)
        {
            Evento? eventoBanco = _context.Evento.FirstOrDefault(eventoAux => eventoAux.EventoId == evento.EventoId);
            if (eventoBanco == null)
                return;

            eventoBanco.Nome = evento.Nome;
            eventoBanco.Localizacao = evento.Localizacao;
            eventoBanco.Inscrição = eventoBanco.Inscrição;
            eventoBanco.StatusEvento = evento.StatusEvento;
            eventoBanco.DataEvento = evento.DataEvento;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Evento? eventoBanco = _context.Evento.FirstOrDefault(eventoAux => eventoAux.EventoId == id);
            if (eventoBanco == null)
                return;


            _context.Remove(eventoBanco);
            _context.SaveChanges();

        }




    }
}
