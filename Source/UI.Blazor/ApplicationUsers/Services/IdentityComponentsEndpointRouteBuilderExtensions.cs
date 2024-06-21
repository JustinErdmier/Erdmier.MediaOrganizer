using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

using Erdmier.MediaOrganizer.Domain.ApplicationUsers.Entities;
using Erdmier.MediaStorage.UI.Blazor.ApplicationUsers.Pages;
using Erdmier.MediaStorage.UI.Blazor.ApplicationUsers.Pages.Manage;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Routing;

internal static class IdentityComponentsEndpointRouteBuilderExtensions
{
    // These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project.
    public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        RouteGroupBuilder accountGroup = endpoints.MapGroup(prefix: "/Account");

        accountGroup.MapPost(pattern: "/PerformExternalLogin",
                             (HttpContext                                     context,
                              [ FromServices ] SignInManager<ApplicationUser> signInManager,
                              [ FromForm ]     string                         provider,
                              [ FromForm ]     string                         returnUrl) =>
                             {
                                 IEnumerable<KeyValuePair<string, StringValues>> query =
                                 [
                                     new KeyValuePair<string, StringValues>(key: "ReturnUrl", returnUrl),
                                     new KeyValuePair<string, StringValues>(key: "Action", ExternalLogin.LoginCallbackAction)
                                 ];

                                 string redirectUrl =
                                     UriHelper.BuildRelative(context.Request.PathBase, path: "/Account/ExternalLogin", query: QueryString.Create(query));

                                 AuthenticationProperties properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

                                 return TypedResults.Challenge(properties, authenticationSchemes: [provider]);
                             });

        accountGroup.MapPost(pattern: "/Logout",
                             async (ClaimsPrincipal user, SignInManager<ApplicationUser> signInManager, [ FromForm ] string returnUrl) =>
                             {
                                 await signInManager.SignOutAsync();

                                 return TypedResults.LocalRedirect(localUrl: $"~/{returnUrl}");
                             });

        RouteGroupBuilder manageGroup = accountGroup.MapGroup(prefix: "/Manage")
                                                    .RequireAuthorization();

        manageGroup.MapPost(pattern: "/LinkExternalLogin",
                            async (HttpContext context, [ FromServices ] SignInManager<ApplicationUser> signInManager, [ FromForm ] string provider) =>
                            {
                                // Clear the existing external cookie to ensure a clean login process
                                await context.SignOutAsync(IdentityConstants.ExternalScheme);

                                string redirectUrl = UriHelper.BuildRelative(context.Request.PathBase,
                                                                             path: "/Account/Manage/ExternalLogins",
                                                                             query: QueryString.Create(name: "Action", ExternalLogins.LinkLoginCallbackAction));

                                AuthenticationProperties properties =
                                    signInManager.ConfigureExternalAuthenticationProperties(provider,
                                                                                            redirectUrl,
                                                                                            userId: signInManager.UserManager.GetUserId(context.User));

                                return TypedResults.Challenge(properties, authenticationSchemes: [provider]);
                            });

        ILoggerFactory loggerFactory  = endpoints.ServiceProvider.GetRequiredService<ILoggerFactory>();
        ILogger        downloadLogger = loggerFactory.CreateLogger(categoryName: "DownloadPersonalData");

        manageGroup.MapPost(pattern: "/DownloadPersonalData",
                            async (HttpContext                                   context,
                                   [ FromServices ] UserManager<ApplicationUser> userManager,
                                   [ FromServices ] AuthenticationStateProvider  authenticationStateProvider) =>
                            {
                                ApplicationUser? user = await userManager.GetUserAsync(context.User);

                                if (user is null)
                                {
                                    return Results.NotFound(value: $"Unable to load user with ID '{userManager.GetUserId(context.User)}'.");
                                }

                                string userId = await userManager.GetUserIdAsync(user);
                                downloadLogger.LogInformation(message: "User with ID '{UserId}' asked for their personal data.", userId);

                                // Only include personal data for download
                                var personalData = new Dictionary<string, string>();

                                IEnumerable<PropertyInfo> personalDataProps = typeof(ApplicationUser).GetProperties()
                                                                                                     .Where(prop => Attribute.IsDefined(prop,
                                                                                                                                        attributeType: typeof(
                                                                                                                                            PersonalDataAttribute)));

                                foreach (PropertyInfo p in personalDataProps)
                                {
                                    personalData.Add(p.Name,
                                                     value: p.GetValue(user)
                                                             ?.ToString() ??
                                                            "null");
                                }

                                IList<UserLoginInfo> logins = await userManager.GetLoginsAsync(user);

                                foreach (UserLoginInfo l in logins)
                                {
                                    personalData.Add(key: $"{l.LoginProvider} external login provider key", l.ProviderKey);
                                }

                                personalData.Add(key: "Authenticator Key", value: (await userManager.GetAuthenticatorKeyAsync(user))!);
                                byte[] fileBytes = JsonSerializer.SerializeToUtf8Bytes(personalData);

                                context.Response.Headers.TryAdd(key: "Content-Disposition", value: "attachment; filename=PersonalData.json");

                                return TypedResults.File(fileBytes, contentType: "application/json", fileDownloadName: "PersonalData.json");
                            });

        return accountGroup;
    }
}
