using EventsRoyalOneSirR.Domains;

namespace EventsRoyalOneSirR.Interfaces
{
    public interface IInscricaoRepository
    {
        public List<Inscrição> Listar();

        public Inscrição? ObterPorId(int id);
        public Inscrição? ObterPorNome(string nome);

        public void Adicionar(Inscrição inscricao, int eventoIds, int participanteIds);
        public void Atualizar(Inscrição inscricao, int eventoIds, int participanteIds);
        public void Remover(int id);



    }
}
