using College.Domain.Abstractions;
using College.Infrastructure.Data;

namespace College.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CollegeDbContext _context;
    public IStudentRepository Students { get; }

    public UnitOfWork(CollegeDbContext context, IStudentRepository students)
    {
        _context = context;
        Students = students;
    }

    public async Task<bool> CommitAsync() => await _context.SaveChangesAsync() > 0;
    public void Dispose() => _context.Dispose();
}
