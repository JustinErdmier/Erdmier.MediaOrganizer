using Erdmier.MediaOrganizer.Domain.ApplicationUsers.Entities;

using Microsoft.AspNetCore.Identity;

namespace Erdmier.MediaStorage.UI.Blazor.ApplicationUsers.Services;

internal sealed class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
{
    public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
    {
        ApplicationUser? user = await userManager.GetUserAsync(context.User);

        if (user is null)
        {
            redirectManager.RedirectToWithStatus(uri: "Account/InvalidUser",
                                                 message: $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.",
                                                 context);
        }

        return user;
    }
}
