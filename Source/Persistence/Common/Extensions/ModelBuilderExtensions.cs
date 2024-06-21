using Erdmier.MediaOrganizer.Persistence.Configurations;

namespace Erdmier.MediaOrganizer.Persistence.Common.Extensions;

public static class ModelBuilderExtensions
{
    public static void ConfigureIdentity(this ModelBuilder modelBuilder)
    {
        new ApplicationUserConfiguration().Configure(builder: modelBuilder.Entity<ApplicationUser>());
        new ApplicationRoleConfiguration().Configure(builder: modelBuilder.Entity<ApplicationRole>());
        new ApplicationUserRoleConfiguration().Configure(builder: modelBuilder.Entity<ApplicationUserRole>());
        new ApplicationUserClaimConfiguration().Configure(builder: modelBuilder.Entity<ApplicationUserClaim>());
        new ApplicationUserLoginConfiguration().Configure(builder: modelBuilder.Entity<ApplicationUserLogin>());
        new ApplicationUserTokenConfiguration().Configure(builder: modelBuilder.Entity<ApplicationUserToken>());
        new ApplicationRoleClaimConfiguration().Configure(builder: modelBuilder.Entity<ApplicationRoleClaim>());
    }
}
