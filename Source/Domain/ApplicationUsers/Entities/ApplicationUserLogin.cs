namespace Erdmier.MediaOrganizer.Domain.ApplicationUsers.Entities;

public sealed class ApplicationUserLogin : IdentityUserLogin<Guid>
{
    public ApplicationUser User { get; set; } = null!;
}
