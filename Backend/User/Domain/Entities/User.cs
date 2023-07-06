using SharedKernel.Interfaces;

namespace User.Domain.Entities;

public class User : IBaseEntity<int>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
