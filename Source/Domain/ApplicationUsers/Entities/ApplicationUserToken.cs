namespace Erdmier.MediaOrganizer.Domain.ApplicationUsers.Entities;

public sealed class ApplicationUserToken : IdentityUserToken<Guid>
{
    public ApplicationUser User { get; set; } = null!;
}
