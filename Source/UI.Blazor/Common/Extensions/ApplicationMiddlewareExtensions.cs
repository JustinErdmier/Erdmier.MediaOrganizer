using Erdmier.MediaStorage.UI.Blazor.Common.Components.Core;

namespace Erdmier.MediaStorage.UI.Blazor.Common.Extensions;

public static class ApplicationMiddlewareExtensions
{
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler(errorHandlingPath: "/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        // TODO: Look into if these calls are necessary - they're not used in the template.
        // app.UseAuthentication();
        // app.UseAuthorization();
        //
        // app.UseRouting();

        app.UseAntiforgery();

        app.MapRazorComponents<App>()
           .AddInteractiveServerRenderMode();

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();

        return app;
    }
}
