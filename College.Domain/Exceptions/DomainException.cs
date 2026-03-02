namespace College.Domain.Exceptions;

public class DomainException: Exception
{
    public DomainException(string Message) : base(Message) { }
}
