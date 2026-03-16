using EventsRoyalOneSirR.Contexts;
using EventsRoyalOneSirR.Domains;
using EventsRoyalOneSirR.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventsRoyalOneSirR.Repository
{
    public class InscricaoRepository : IInscricaoRepository
    {

        private readonly EventsRoyalOneSirRContext _context;

        public InscricaoRepository(EventsRoyalOneSirRContext context)
        {
            _context = context;
        }

        public List<Inscrição> Listar()
        {
            List<Inscrição> inscricao = _context.Inscrição
                .Include(inscricaoAux => inscricaoAux.Evento)
                .Include(inscricaoAux => inscricaoAux.Usuario)
                .ToList();

            return inscricao;
        }

        public Inscrição? ObterPorId(int id)
        {
            Inscrição? inscricao = _context.Inscrição
           .Include(inscricaoAux => inscricaoAux.Evento)
           .Include(inscricaoAux => inscricaoAux.Usuario)
           .FirstOrDefault(inscricaoId => inscricaoId.InscriçãoId == id);

            return inscricao;
        }

        

        public void Adicionar(Inscrição inscricao, int eventoIds, int participanteIds)
        {
            Evento? inscricaoQueryEvento = _context.Evento.FirstOrDefault
                (eventoAux => eventoAux.EventoId == eventoIds);

            Usuario? inscricaoQueryUsuario = _context.Usuario.FirstOrDefault
                (participanteAux => participanteAux.UsuarioId == participanteIds);

            if (inscricaoQueryEvento == null && inscricaoQueryUsuario == null)
                return;

            inscricao.Evento = inscricaoQueryEvento;
            inscricao.Usuario = inscricaoQueryUsuario;

            _context.Inscrição.Add(inscricao);
            _context.SaveChanges();
        }

        public void Atualizar(Inscrição inscricao, int eventoIds, int participanteIds)
        {
            Evento? evento = _context.Evento.FirstOrDefault(eventoAux => eventoAux.EventoId == eventoIds);
            Usuario? usuario = _context.Usuario.FirstOrDefault(usuarioAux => usuarioAux.UsuarioId == participanteIds);

            if (usuario == null || evento == null)
                return;

            inscricao.UsuarioId = usuario.UsuarioId;
            inscricao.EventoId = evento.EventoId;

            _context.SaveChanges();


        }

        public void Remover(int id)
        {
            Inscrição? inscricao = _context.Inscrição.FirstOrDefault(inscricaoAux => inscricaoAux.InscriçãoId == id);

            if (inscricao == null)
                return;

            _context.Inscrição.Remove(inscricao);
            _context.SaveChanges();

        }


    }
}
