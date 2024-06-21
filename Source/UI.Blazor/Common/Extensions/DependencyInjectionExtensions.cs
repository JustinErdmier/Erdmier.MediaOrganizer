using System.Net;

using Erdmier.MediaOrganizer.Domain.ApplicationUsers.Entities;
using Erdmier.MediaOrganizer.Persistence.Contexts;
using Erdmier.MediaStorage.UI.Blazor.ApplicationUsers.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Erdmier.MediaStorage.UI.Blazor.Common.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddWebApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorComponents()
                .AddInteractiveServerComponents();

        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        services.AddScoped<CurrentUserDetails>();

        // TODO: Look into Hsts again...
        services.AddHsts(options =>
        {
            options.Preload           = true;
            options.IncludeSubDomains = true;

            // The default HSTS value is 30 days.
            options.MaxAge = TimeSpan.FromDays(value: 1);
        });

        // TODO: Research Https Redirection and if the Https Port matters.
        services.AddHttpsRedirection(options =>
        {
            options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
            options.HttpsPort          = 5001;
        });

        return services;
    }

    public static IServiceCollection AddIdentityAndAuth(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
                {
                    options.DefaultScheme              = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme        = IdentityConstants.ExternalScheme;
                    options.RequireAuthenticatedSignIn = true;
                })
                .AddIdentityCookies();

        services.AddAuthorizationBuilder()
                .SetFallbackPolicy(policy: new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                                           .Build());

        services.AddIdentityCore<ApplicationUser>(options =>
                {
                    // Sign In settings.
                    options.SignIn.RequireConfirmedAccount = true;

                    // Password settings.
                    options.Password.RequireDigit           = true;
                    options.Password.RequireLowercase       = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase       = true;
                    options.Password.RequiredLength         = 8;
                    options.Password.RequiredUniqueChars    = 1;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(value: 20);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers      = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-._@+";

                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/AccessDenied";
            options.Cookie.Name      = "FlrDigitalSolutionsBustANutCookie";
            options.Cookie.HttpOnly  = true;
            options.ExpireTimeSpan   = TimeSpan.FromMinutes(value: 60);
            options.LoginPath        = "/Account/Login";

            // ReturnUrlParameter requires
            // using Microsoft.AspNetCore.Authentication.Cookies;
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration  = true;
        });

        // TODO: Move this to Infrastructure.
        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        return services;
    }
}
