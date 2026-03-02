using College.Domain.Abstractions;

namespace College.Domain.Entities;

public class Student: BaseEntity
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }

    public Student(string firstName, string email)
    {
        FirstName = firstName;
        Email = email;
    }
   
}
