using SharedKernel.Interfaces;

namespace Identity.Persistence.EDMs;

public class IdentityUser : IBaseEntity<int>, IAuditableEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
