namespace Erdmier.MediaOrganizer.Domain.ApplicationUsers.Entities;

public sealed class ApplicationUserClaim : IdentityUserClaim<Guid>
{
    public ApplicationUser User { get; set; } = null!;
}
