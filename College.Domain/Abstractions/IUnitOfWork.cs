namespace College.Domain.Abstractions;
public interface IUnitOfWork: IDisposable
{
    IStudentRepository Students { get; }

    Task<bool> CommitAsync();
}
