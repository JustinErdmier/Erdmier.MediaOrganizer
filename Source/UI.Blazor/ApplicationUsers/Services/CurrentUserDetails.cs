using Erdmier.MediaOrganizer.Domain.ApplicationUsers.Entities;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Erdmier.MediaStorage.UI.Blazor.ApplicationUsers.Services;

public sealed class CurrentUserDetails
{
    private readonly ApplicationUser? _currentUser;

    // TODO: Try moving the the user to a private field and set it in the constructor. Test if this will cause issues across multiple requests/circuits/tabs/users, etc.

    public CurrentUserDetails(AuthenticationStateProvider authenticationStateProvider, UserManager<ApplicationUser> userManager)
    {
        AuthenticationState authState = authenticationStateProvider.GetAuthenticationStateAsync()
                                                                   .GetAwaiter()
                                                                   .GetResult();

        _currentUser = userManager.GetUserAsync(authState.User)
                                  .GetAwaiter()
                                  .GetResult();
    }

    public Guid GetUserId() => _currentUser?.Id ?? Guid.Empty;

    public string? GetUserName() => _currentUser?.UserName;
}
