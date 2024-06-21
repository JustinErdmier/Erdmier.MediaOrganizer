namespace Erdmier.MediaOrganizer.Domain.ApplicationUsers.Entities;

public sealed class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public ApplicationRole Role { get; set; } = null!;
}
