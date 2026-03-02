using College.Domain.Abstractions;

namespace College.Infrastructure.Repositories;

public class UnitOfWork: IUnitOfWork
{
    public IStudentRepository Students { get; }

    public UnitOfWork(IStudentRepository students)
    {
        Students = students;
    }

    public async Task<bool> CommitAsync()
    {
        return await Task.FromResult(true); // DB exemple, always successful
    }

    public void Dispose() { }
}
