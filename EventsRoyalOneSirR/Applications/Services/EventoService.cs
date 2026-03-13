using EventsRoyalOneSirR.Applications.DTOs.EventoDTO;
using EventsRoyalOneSirR.Domains;
using EventsRoyalOneSirR.Interfaces;
using EventsRoyalOneSirR.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace EventsRoyalOneSirR.Applications.Services
{
    public class EventoService
    {

        private readonly IEventoRepository _repository;

        public EventoService(IEventoRepository repository)
        {
            _repository = repository;
        }

        private static LerEventoDTO LerDTOs(Evento evento)
        {
            LerEventoDTO lerDto = new LerEventoDTO
            {
                Nome = evento.Nome,
                Localização = evento.Localizacao,
                DataEvento = evento.DataEvento,
                StatusEvento = evento.StatusEvento ?? true
            };

            return lerDto;
        }

        public List<LerEventoDTO> Listar()
        {
            List<Evento> eventoListar = _repository.Listar();
            List<LerEventoDTO> lerDtoEvento = eventoListar.Select
                (eventoQuery => LerDTOs(eventoQuery)).ToList();
            return lerDtoEvento;
        }

        public LerEventoDTO ObterPorId(int id)
        {
            Evento? evento = _repository.ObterPorId(id);
            if (evento == null)
                throw new DomainException("Este evento não existe");

            return LerDTOs(evento);
        }

        public LerEventoDTO ObterPorNome(string nome)
        {
            Evento? evento = _repository.ObterPorNome(nome);
            if (evento == null)
                throw new DomainException("Este evento não existe");

            return LerDTOs(evento);
        }

        private static void validarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new DomainException("Nome inválido");
        }

        public LerEventoDTO Adicionar(CriarEventoDTO criarDTO)
        {
            validarNome(criarDTO.Nome);

            if (string.IsNullOrEmpty(criarDTO.Nome))
                throw new DomainException("Evento não existe");

            Evento? evento = new Evento
            {
                Nome = criarDTO.Nome,
                Localizacao = criarDTO.Localização,
                DataEvento = criarDTO.DataEvento

            };

            return LerDTOs(evento);
        }


        public LerEventoDTO Atualizar(int id, CriarEventoDTO criarDTO)
        {
            Evento? eventoBanco = _repository.ObterPorId(id);
            if (eventoBanco == null)
                throw new DomainException("Evento não existe");

            validarNome(eventoBanco.Nome);

            eventoBanco.Nome = criarDTO.Nome;
            eventoBanco.Localizacao = criarDTO.Localização;
            eventoBanco.DataEvento = criarDTO.DataEvento;

            _repository.Atualizar(eventoBanco);
            return LerDTOs(eventoBanco);
        }

        public LerEventoDTO Remover(int id)
        {
            Evento evento = _repository.ObterPorId(id);
            if(evento == null)
            {
                throw new DomainException("Evento não existente");
            }

            _repository.Remover(id);
            return LerDTOs(evento);
        }

    }
}

