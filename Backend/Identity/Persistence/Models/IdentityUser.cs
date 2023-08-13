using SharedKernel.Interfaces;

namespace Identity.Persistence.Models;

public class IdentityUser : IBaseEntity<int>, IAuditableEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
