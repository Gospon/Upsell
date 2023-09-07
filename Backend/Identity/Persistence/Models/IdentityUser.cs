using SharedKernel.Interfaces;

namespace Identity.Persistence.Models;

public class IdentityUser : AuditableEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}
