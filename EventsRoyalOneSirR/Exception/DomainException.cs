using EventsRoyalOneSirR.Exceptions;

namespace EventsRoyalOneSirR.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string mensagem) : base(mensagem) { }

    }
}
