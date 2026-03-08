using College.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace College.Infrastructure.Data;

public class CollegeDbContext : DbContext
{
    public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Email).IsRequired();
            builder.HasIndex(s => s.Email).IsUnique(); 
        });

        base.OnModelCreating(modelBuilder);
    }
}